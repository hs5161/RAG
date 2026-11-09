using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C___teacher
{
    internal class Stack<T>
    {
        Node<T> head;
        public Stack() { }
        public void Push(T item) => head = new Node<T>(item, head);//דוחפת לראש הרשימה
       public T Pop()
        {
            if (head == null)
                throw new Exception("empty stack");
            T item = head.GetValue;
            head = head.GetNext;
            return item;    
        }
        public T Top()
        {
            if (head == null)
                throw new Exception("empty stack");
            return head.GetValue;   

        }
        public bool IsEmpty() { return head == null; }
        public override string ToString()
        { string str = "";
            Node<T> temp = head;
            while(temp != null)
            {
                str += temp.ToString()+" ";
                temp = temp.GetNext; 

            }
            return str;
        }
    }
}
