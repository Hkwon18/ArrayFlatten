using System;
using System.Collections.Generic;
using System.Text;
using ArrayFlatten.Model;

namespace ArrayFlatten.ViewModel
{
    internal class TreeViewModel
    {
        public TreeViewModel()
        {
            int[][] array = { new [] {1}, new [] { 1, 2, 3}};

            //TODO: Convert nested in arrays into nodes

            Root = Treeify(array);
            int[] flatArray = Root.FlattenToArray();
        }

        public TreeNode<int[]> Root { get; set; }

        private TreeNode<int[]> Treeify(Array nestedArray)
        {
            TreeNode<int[]> root = new TreeNode<int[]>();
            List<int> nodeInts = new List<int>();
            foreach (var num in nestedArray)
            {
                if (num is int)
                {
                    nodeInts.Add((int) num);
                }
                else if (num is Array)
                {
                    root.AddChild(Treeify((Array) num));
                }
            }
            root.Item = nodeInts.ToArray();
            return root;
        }
    }
}
