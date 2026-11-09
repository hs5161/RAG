using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C___teacher
{
    internal class NodeTree
    {
        private BinNode<int> treeNode;
        private int numRange;

        public NodeTree(BinNode<int> t, int x)
        {
            treeNode = t;
            numRange = x;
        }
        public BinNode<int> GetTree()
        {
            return this.treeNode;
        }
        public void SetTree(BinNode<int> t)
        {
            this.treeNode = t;
        }
        public int getRange()
        {
            return numRange;
        }
        public String toString()
        {
            return this.numRange + " ";
        }
}

}
