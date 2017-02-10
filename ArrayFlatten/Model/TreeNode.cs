using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace ArrayFlatten.Model
{
    class TreeNode<T> : INotifyPropertyChanged
    {
        public TreeNode()
        {
        }

        public TreeNode(int[] nodeInts)
        {
            NodeInts = nodeInts;
        }

        public void AddChild(TreeNode<T> child)
        {
            Children.Add(child);
        }

        public void AddChild(int[] item)
        {
            TreeNode<T> nodeItem = new TreeNode<T>(item);
            Children.Add(nodeItem);
        }

        public List<TreeNode<T>> Children = new List<TreeNode<T>>();

        public int[] NodeInts { get; set; }

        public string ArrayToString()
        {
            return string.Join(", ", FlattenTreeToArray());
        }

        public int[] FlattenTreeToArray()
        {
            return Listify().ToArray();
        }

        public List<int> Listify()
        {
            List<int> flatList = new List<int>(NodeInts);
            foreach (TreeNode<T> child in Children)
            {
                flatList.AddRange(child.Listify());
            }
            return flatList;
        }

        #region INotifyPropertyChanged 
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
