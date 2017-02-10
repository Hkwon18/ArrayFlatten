using System.Collections.Generic;


namespace ArrayFlatten.Models
{
    class TreeNode<T>
    {
        public TreeNode()
        {
        }

        public TreeNode(int nodeInt)
        {
            NodeInt = nodeInt;
        }

        public void AddChild(TreeNode<T> child)
        {
            Children.Add(child);
        }

        public void AddChild(int item)
        {
            TreeNode<T> nodeItem = new TreeNode<T>(item);
            Children.Add(nodeItem);
        }

        public List<TreeNode<T>> Children = new List<TreeNode<T>>();

        private bool _didNodeIntChange = false;
        private int _nodeInt;
        public int NodeInt
        {
            get
            {
                return _nodeInt;
            }
            set
            {
                _nodeInt = value;
                _didNodeIntChange = true;
            }
        }

        public string ArrayToString()
        {
            return string.Join(" ", FlattenTreeToArray());
        }

        public int[] FlattenTreeToArray()
        {
            return Listify().ToArray();
        }

        public List<int> Listify()
        {
            List<int> flatList = new List<int>();
            if (_didNodeIntChange)
            {
                flatList.Add(NodeInt);
            }
            else
            {
                if (Children != null)
                {
                    foreach (TreeNode<T> child in Children)
                    {
                        flatList.AddRange(child.Listify());
                    }
                }
            }
            return flatList;
        }
    }
}
