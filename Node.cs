using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C___teacher
{
    internal class Node<T>

    {
        T value;
        Node<T> next;

        public Node(T value, Node<T> next)
        {
            this.value = value;
            this.next = next;
        }

        public Node(T value)
        {
            this.value = value;
        }

        public void SetValue(T value) => this.value = value;
        public T GetValue => this.value;
        public void SetNext(Node<T> next) => this.next = next;
        public Node<T> GetNext => this.next;
        public bool HasNext => this.next != null;
        public override string ToString()
        {
            return value.ToString();
        }

      
    }
}
