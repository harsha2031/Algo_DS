using System;
namespace Algo_DS.Models
{
    public class TreeNode
    {
        public int value;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val)
        {
            this.value = val;
            this.left = null;
            this.right = null;
        }
    }
}
