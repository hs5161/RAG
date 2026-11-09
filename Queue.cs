using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C___teacher
{
    internal class Queue<T>
    {

        Node<T> head, tail;

        public void Insert(T item)
        {
            if (head == null)
                head = tail = new Node<T>(item);
            else
            {
                tail.SetNext(new Node<T>(item));//הוספה לסוף התור
                tail = tail.GetNext;//קידום מצביע של סוף התור לאיבר האחרון 
            }
        }

        public T Remove()//מחיקה מהתור 
        {
            if (head == null)
                throw new Exception("empty queue");
            T item = head.GetValue;
            head = head.GetNext;
            return item;
        }
        public T Head()
        {
            if (head == null)
                throw new Exception("empty stack");
            return head.GetValue;

        }
        public bool IsEmpty() { return head == null; }
        public override string ToString()
        {
            string str = "";
            Node<T> temp = head;
            while (temp != null)
            {
                str += temp.ToString() + " ";
                temp = temp.GetNext;

            }
            return str;
        }
    }
}
