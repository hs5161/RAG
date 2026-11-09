# Technical Specification: C# Data Structures Project

## 1. Project Overview

| Property | Value |
|----------|--------|
| **Solution** | C#- teacher.sln |
| **Project** | C#- teacher |
| **Namespace** | `C___teacher` |
| **Target Framework** | .NET 8.0 |
| **Output Type** | Executable (Console) |
| **Language** | C# (ImplicitUsings, Nullable enabled) |

---

## 2. Architecture Summary

The project implements classic data structures using custom linked nodes (no `System.Collections.Generic` stack/queue/tree):

- **Stack&lt;T&gt;** — LIFO, singly linked list (head only).
- **Queue&lt;T&gt;** — FIFO, singly linked list (head + tail).
- **Tree** — Binary tree via **BinNode&lt;T&gt;**; **NodeTree** wraps a `BinNode<int>` with a numeric range for list-of-trees scenarios.

All types are `internal` and live in the same assembly and namespace.

---

## 3. Core Types

### 3.1 Node&lt;T&gt; (Linked List Node)

**File:** `Node.cs`  
**Role:** Single node in a singly linked list; used as the backing store for `Stack<T>` and `Queue<T>`.

| Member | Type | Description |
|--------|------|-------------|
| **Constructors** | | |
| `Node(T value, Node<T> next)` | — | Value and next pointer. |
| `Node(T value)` | — | Value only; `next` is null. |
| **Properties / accessors** | | |
| `GetValue` | `T` | Stored value. |
| `GetNext` | `Node<T>` | Next node (may be null). |
| `HasNext` | `bool` | True if `GetNext != null`. |
| **Methods** | | |
| `SetValue(T value)` | void | Set stored value. |
| `SetNext(Node<T> next)` | void | Set next node. |
| `ToString()` | string | Delegates to `value.ToString()`. |

**Implementation notes:**  
- No generic constraints on `T`.  
- List structure is built and traversed only via `Node<T>` references (no separate list class).

---

### 3.2 Stack&lt;T&gt;

**File:** `Stack.cs`  
**Role:** LIFO stack implemented as a singly linked list with a single head reference.

| Member | Signature | Description |
|--------|-----------|-------------|
| **Field** | `Node<T> head` | Top of stack (null when empty). |
| **Constructor** | `Stack()` | Empty stack. |
| **Methods** | | |
| `Push(T item)` | void | Inserts at head (new node with current `head` as next). |
| `Pop()` | `T` | Removes and returns top; throws if empty. |
| `Top()` | `T` | Returns top without removing; throws if empty. |
| `IsEmpty()` | `bool` | True when `head == null`. |
| `ToString()` | string | Left-to-right from top to bottom (space-separated). |

**Time complexity:** Push O(1), Pop O(1), Top O(1).  
**Error handling:** `Pop()` and `Top()` throw `Exception` with message `"empty stack"` when the stack is empty.

---

### 3.3 Queue&lt;T&gt;

**File:** `Queue.cs`  
**Role:** FIFO queue implemented as a singly linked list with head and tail.

| Member | Signature | Description |
|--------|-----------|-------------|
| **Fields** | `Node<T> head, tail` | Front and back of queue. |
| **Methods** | | |
| `Insert(T item)` | void | Enqueue at tail; if empty, sets both head and tail to new node. |
| `Remove()` | `T` | Dequeues from head and returns value; throws if empty. |
| `Head()` | `T` | Peek front element; throws if empty. |
| `IsEmpty()` | `bool` | True when `head == null`. |
| `ToString()` | string | Front to back, space-separated. |

**Time complexity:** Insert O(1), Remove O(1), Head O(1).  
**Error handling:** `Remove()` and `Head()` throw `Exception` (`Head()` message says `"empty stack"` — copy-paste from Stack).

---

### 3.4 BinNode&lt;T&gt; (Binary Tree Node)

**File:** `BinNode.cs`  
**Role:** Node of a binary tree; implements `IComparable<BinNode<T>>`. Used as the building block for trees and in `NodeTree`.

**Constraint:** `where T : IComparable<T>`.

| Member | Signature | Description |
|--------|-----------|-------------|
| **Fields** | `T value`, `BinNode<T> right`, `BinNode<T> left` | Stored value and left/right children. |
| **Constructors** | | |
| `BinNode(T value, BinNode<T> right, BinNode<T> left)` | — | Full initialization. |
| `BinNode(T value)` | — | Value only; both children null. |
| **Accessors** | | |
| `GetValue()` | `T` | Stored value. |
| `GetLeft` / `GetRight` | `BinNode<T>` | Left/right child. |
| `HasLeft` / `HasRight` | `bool` | True if left/right is non-null. |
| **Mutators** | | |
| `SetValue(T value)` | void | Set value. |
| `SetLeft(BinNode<T> n)` / `SetRight(BinNode<T> n)` | void | Set left/right child. |
| **Other** | | |
| `ToString()` | string | Delegates to `value.ToString()`. |
| `CompareTo(BinNode<T>? other)` | int | Delegates to `value.CompareTo(other.value)`. |

**Usage:** Tree algorithms (e.g. BST insert, in-order print) are implemented in `Program.cs` (commented) and operate on `BinNode<int>` (or generic `BinNode<T>`).

---

### 3.5 NodeTree (Tree + Range Wrapper)

**File:** `NodeTree.cs`  
**Role:** Wrapper that holds a binary tree of integers and a numeric “range” value (e.g. bucket identifier). Used in list-of-trees structures where each list node holds a tree and its range.

| Member | Signature | Description |
|--------|-----------|-------------|
| **Fields** | `BinNode<int> treeNode`, `int numRange` | Root of tree and range id. |
| **Constructor** | `NodeTree(BinNode<int> t, int x)` | Sets tree root and range. |
| **Methods** | | |
| `GetTree()` / `SetTree(BinNode<int> t)` | BinNode&lt;int&gt; / void | Get/set tree root. |
| `getRange()` | int | Returns `numRange`. |
| `toString()` | string | Returns string representation of `numRange` (note: lowercase `toString`, not `ToString`). |

**Typical use (from commented code):** A linked list `Node<NodeTree>` where each node’s `NodeTree` holds a BST for a given range (e.g. 100–199, 200–299); numbers are inserted into the appropriate list node’s tree by range.

---

## 4. Entry Point and Program Logic

**File:** `Program.cs`

- **Commented block (#region techer):**  
  - Stack reversal using a queue.  
  - Linked-list exercises (e.g. difference list, print).  
  - Binary search tree: `AddItem`, in-order `PrintIn`, `ReturnFullBinaryTree` / `Func`.  
  - Tree exercises: `IsExist`, `IsExist1`, `IsSpecialTree`.  
  - List-of-trees construction and print: `BuildListNodeTtee`, `print`, using `Node<NodeTree>` and range-based buckets.

- **Active code:**  
  - `aad(Stack<string> s, char c)` — finds first string in stack that contains `c`, appends `c` to it (using a temp stack); if none, pushes `c.ToString()`.  
  - `check(Node<int> list, int[] arr)` — stub (returns false).  
  - `copy(Node<int> list, int[] arr)` — stub (returns null).  
  - `ex1(Node<int> list, int[] arr)` — incomplete (iterates and calls `check`; can throw if `list` or `list.GetNext` is null).

---

## 5. Build and Solution Layout

- **Solution:** `C#- teacher.sln` — single project, Debug|Any CPU and Release|Any CPU.  
- **Project:** `C#- teacher.csproj` — SDK-style, `net8.0`, `Exe`, `RootNamespace` and `C#- teacher` project name.  
- **Source files (root):** `Program.cs`, `Node.cs`, `Stack.cs`, `Queue.cs`, `BinNode.cs`, `NodeTree.cs`.  
- **Generated:** `obj/` (e.g. AssemblyInfo, GlobalUsings) — standard .NET output.

---

## 6. Dependencies

- **Framework:** .NET 8.0 only; no extra NuGet packages.  
- **BCL usage:** `System`, `System.Collections.Generic`, `System.Linq`, `System.Text`, `System.Threading.Tasks`; `BinNode` also uses `System.Reflection.Metadata.Ecma335` (likely unused).

---

## 7. Conventions and Notes

- **Naming:** PascalCase for types and public members; `getRange` / `toString` in `NodeTree` are camelCase.  
- **Encapsulation:** All data-structure types are `internal`; fields are private with get/set methods.  
- **Errors:** Stack/Queue use `throw new Exception("...")` for empty state; no custom exception types.  
- **Thread safety:** No synchronization; single-threaded usage assumed.  
- **Comments:** Hebrew comments in source (e.g. “דוחפת לראש הרשימה”) describe list/stack/queue behavior.

---

## 8. Quick Reference: Public API

```text
Node<T>         : (value, next?) → GetValue, GetNext, HasNext, SetValue, SetNext, ToString
Stack<T>        : Push, Pop, Top, IsEmpty, ToString
Queue<T>        : Insert, Remove, Head, IsEmpty, ToString
BinNode<T>      : T : IComparable<T>; value, left, right; Get/Set value & children; HasLeft, HasRight; CompareTo, ToString
NodeTree        : BinNode<int> + int range; GetTree, SetTree, getRange, toString
```

This document is the single source of truth for the project’s structure, types, and behavior as implemented in the codebase.
