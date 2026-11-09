using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace C___teacher
{
    internal class BinNode<T> : IComparable<BinNode<T>>
     where T : IComparable<T>
    {
        T value;
        BinNode<T> right;
        BinNode<T> left;
        public BinNode(T value, BinNode<T> right, BinNode<T> left)
        {
            this.value = value;
            this.right = right;
            this.left = left;
        }

        public BinNode(T value)
        {
            this.value = value;
        }
        public void SetValue(T value) => this.value = value;
        public T GetValue() => this.value;
        public void SetLeft(BinNode<T> n) => this.left = n;
        public void SetRight(BinNode<T> n) => this.right = n;
        public BinNode<T> GetLeft => left;
        public BinNode<T> GetRight => right;
        public bool HasRight => right != null;
        public bool HasLeft => left != null;
        public override string ToString()
        {
            return value.ToString();
        }

        public int CompareTo(BinNode<T>? other)
        {
            return value.CompareTo(other.value);
        }
    }
}
