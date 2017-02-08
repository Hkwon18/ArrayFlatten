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

        public TreeNode(int[] item)
        {
            Item = item;
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

        public int[] Item { get; set; }

        private string _arrayString = string.Empty;
        public string ArrayString
        {
            get
            {
                return PrintTree();
            }
            set
            {
                _arrayString = value;
            }
        }

        public int[] FlattenToArray()
        {
            return Listify().ToArray();
        }

        public List<int> Listify()
        {
            List<int> flatList = new List<int>(Item);
            foreach (TreeNode<T> child in Children)
            {
                flatList.AddRange(child.Listify());
            }
            return flatList;
        }

        public string PrintTree()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(PrintNode());
            foreach (TreeNode<T> child in Children)
            {
                sb.Append(child.PrintTree());
            }
            return sb.ToString();
        }

        public string PrintNode()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var i in (IEnumerable) Item)
            {
                sb.Append(i);
                sb.Append(", ");
            }
            return sb.ToString();
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
