import os
import json
from typing import List
from pydantic import BaseModel, Field
from dotenv import load_dotenv

# LlamaIndex & Integrations
from llama_index.core import (
    SimpleDirectoryReader, 
    StorageContext, 
    VectorStoreIndex, 
    Settings
)
from llama_index.embeddings.cohere import CohereEmbedding
from llama_index.llms.cohere import Cohere
from llama_index.vector_stores.pinecone import PineconeVectorStore
from llama_index.core.workflow import Workflow, step, StartEvent, StopEvent, Event
from llama_index.core.schema import NodeWithScore
from llama_index.core.program import LLMTextCompletionProgram
import gradio as gr
from pinecone import Pinecone, ServerlessSpec

# 1. Settings and connections (ensure a .env file exists)
load_dotenv()

Settings.embed_model = CohereEmbedding(
    api_key=os.environ.get("COHERE_API_KEY"),
    model_name="embed-multilingual-v3.0"
)

Settings.llm = Cohere(
    api_key=os.environ.get("COHERE_API_KEY"),
    model="command-r-08-2024" 
)

# 2. Extended schema definition (at least 3 categories as required)
class TechnicalDecision(BaseModel):
    title: str = Field(description="Brief title of the decision")
    decision: str = Field(description="The actual choice made")
    tags: List[str] = Field(description="Relevant tags like 'database', 'frontend'")

class ProjectWarning(BaseModel):
    area: str = Field(description="The component or feature affected")
    message: str = Field(description="The specific warning or restriction")
    severity: str = Field(description="low, medium, or high")

class TechnicalRule(BaseModel):  # Additional third category
    rule: str = Field(description="The specific guideline or coding rule")
    context: str = Field(description="Where or when this rule applies")

class ExtractionResult(BaseModel):
    decisions: List[TechnicalDecision]
    warnings: List[ProjectWarning]
    rules: List[TechnicalRule]

# 3. Events for the workflow
class RoutingEvent(Event):
    choice: str 
    query: str

class RetrievedEvent(Event):
    nodes: list[NodeWithScore]
    query: str

# 4. The Agentic RAG Workflow
class AgenticRAGWorkflow(Workflow):
    def __init__(self, index, **kwargs):
        super().__init__(**kwargs)
        self.index = index

    @step
    async def route_query(self, ev: StartEvent) -> RoutingEvent | StopEvent:
        query = ev.query
        if not query or not query.strip():
            return StopEvent(result="Please provide a valid query for search.")
        
        query_l = query.lower()
        # Routing logic: list/rules queries are directed to the structured dataset
        structured_keywords = ["list", "decisions", "warnings", "rules", "all"]
        if any(word in query_l for word in structured_keywords):
            return RoutingEvent(choice="structured", query=query)
        
        return RoutingEvent(choice="semantic", query=query)

    @step
    async def retrieve_semantic(self, ev: RoutingEvent) -> RetrievedEvent | None:
        if ev.choice != "semantic": return None
        
        print(f"🔍 Semantic Search: {ev.query}")
        retriever = self.index.as_retriever(similarity_top_k=3)
        nodes = retriever.retrieve(ev.query)
        return RetrievedEvent(nodes=nodes, query=ev.query)

    @step
    async def retrieve_structured(self, ev: RoutingEvent) -> StopEvent | None:
        if ev.choice != "structured": return None
        
        print(f"📊 Structured Retrieval: {ev.query}")
        if not os.path.exists("extracted_data.json"):
            return StopEvent(result="Error: structured data file not found. Run the extraction process first.")
        
        with open("extracted_data.json", "r", encoding="utf-8") as f:
            data = json.load(f)
        
        return StopEvent(result=f"Found the following structured data:\n{json.dumps(data, indent=2, ensure_ascii=False)}")

    @step
    async def synthesize(self, ev: RetrievedEvent) -> StopEvent:
        if not ev.nodes:
            return StopEvent(result="No relevant information found in documents.")
            
        print("✍️ Composing final answer...")
        query_engine = self.index.as_query_engine()
        response = query_engine.synthesize(ev.query, ev.nodes)
        return StopEvent(result=str(response))

# 5. Data extraction and setup
def setup_index():
    # Logic for loading documents from Cursor, Claude, Kiro
    input_configs = [
        {"path": "../.cursor", "tool": "cursor"},
        {"path": "../.claude", "tool": "claude"},
        {"path": "../.kiro", "tool": "kiro"}
    ]
    
    documents = []
    for config in input_configs:
        if os.path.exists(config["path"]):
            reader = SimpleDirectoryReader(input_dir=config["path"], recursive=True, required_exts=[".md"])
            curr_docs = reader.load_data()
            for doc in curr_docs: doc.metadata["tool_source"] = config["tool"]
            documents.extend(curr_docs)
    
    # Connection to Pinecone
    pc = Pinecone(api_key=os.getenv("PINECONE_API_KEY"))
    index_name = "coding-agents-docs"
    
    # Create index if not exists
    if index_name not in [idx.name for idx in pc.list_indexes()]:
        pc.create_index(name=index_name, dimension=1024, metric="cosine", 
                        spec=ServerlessSpec(cloud="aws", region="us-east-1"))

    pinecone_index = pc.Index(index_name)
    vector_store = PineconeVectorStore(pinecone_index=pinecone_index)
    storage_context = StorageContext.from_defaults(vector_store=vector_store)
    
    return VectorStoreIndex.from_documents(documents, storage_context=storage_context), documents

async def run_data_extraction(documents):
    """Run structured data extraction and save to a JSON file"""
    program = LLMTextCompletionProgram.from_defaults(
        output_cls=ExtractionResult,
        prompt_template_str="Review the technical notes and extract ALL decisions, warnings, and rules:\n{context_str}",
        llm=Settings.llm
    )
    # Use more extensive text for broader coverage
    context_text = "\n".join([doc.text for doc in documents[:15]]) 
    structured_data = program(context_str=context_text)
    
    with open("extracted_data.json", "w", encoding="utf-8") as f:
        json.dump(structured_data.dict(), f, indent=4, ensure_ascii=False)
    print("✅ Structured data saved to extracted_data.json")

# 6. Run and Gradio Interface
async def main():
    index, docs = setup_index()
#    await run_data_extraction(docs) 
    rag_workflow = AgenticRAGWorkflow(index=index, timeout=60)

    async def chat_fn(message, history):
        response = await rag_workflow.run(query=message)
        return str(response)

    demo = gr.ChatInterface(fn=chat_fn, title="Coding Agents Knowledge Hub")
    demo.launch()

if __name__ == "__main__":
    import asyncio
    asyncio.run(main())