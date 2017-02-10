using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using ArrayFlatten.Model;
using ArrayFlatten.Command;

namespace ArrayFlatten.ViewModel
{
    internal class TreeViewModel
    {
        public TreeViewModel()
        {
            Object[] array = { new [] {1}, new [] { 1, 2, 3}};
            Root = Treeify(array);
            CheckCommand = new CheckTreeCommand(this);
        }

        public TreeNode<int[]> Root { get; set; }

        private string _inputArrayString;
        public string InputArrayString
        {
            get
            {
                return _inputArrayString;
            }
            set
            {
                _inputArrayString = value;
            }
        }

        private string _outputArrayString = string.Empty;
        public string OutputArrayString
        {
            get
            {
                return Root.ArrayToString();
            }
            set
            {
                _outputArrayString = value;
            }
        }

        public bool CanFlattenInput
        {
            get
            {
                return IsValidArrayString(InputArrayString);
            }
        }

        public bool IsValidArrayString(string input)
        {
            Stack<char> stack = new Stack<char>();
            bool success = false;
            if (String.IsNullOrWhiteSpace(InputArrayString))
            {
                return success;
            }
            int i = 0;
            while (i < input.Length)
            {
                if (input[i] == '[')
                {
                    stack.Push(input[i]);
                }
                else if (input[i] == ']')
                {
                    if (stack.Count == 0)
                    {
                        return success;
                    }
                    stack.Pop();
                }
                else if (!char.IsNumber(input, i) && input[i] != ' ' && input[i] != ',')
                {
                    return success;
                }
                i++;
            }
            success = (stack.Count == 0);
            return success;
        }

        public Array WrapArrayify(string inputString)
        {
            Array array = Arrayify(inputString).ToArray();
            return array;
        }

        public List<object> Arrayify(string inputString)
        {
            List<object> arrayList = new List<object>();
            int i = 0;
            while (i < inputString.Length)
            {
                if (inputString[i] == '[')
                {
                    Stack<char> bracketStack = new Stack<char>();
                    bracketStack.Push(inputString[i]);
                    int endBracketIndex = i + 1;
                    while (bracketStack.Count > 0)
                    {
                        if (inputString[endBracketIndex] == '[')
                        {
                            bracketStack.Push(inputString[endBracketIndex]);
                        }
                        if (inputString[endBracketIndex] == ']')
                        {
                            bracketStack.Pop();
                        }
                        if (bracketStack.Count > 0)
                        {
                            endBracketIndex++;
                        }   
                    }
                    string insideString = inputString.Substring(i + 1, endBracketIndex-i-1);
                    arrayList.Add(Arrayify(insideString).ToArray());
                    i = endBracketIndex;
                }
                else if (char.IsNumber(inputString[i]))
                {
                    int bracketIndex = inputString.Substring(i).IndexOf(']');
                    int leftIndex = inputString.Substring(i).IndexOf('[');
                    if (leftIndex > 0 && bracketIndex > 0)
                    {
                        bracketIndex = Math.Min(bracketIndex, leftIndex);
                    }
                    else if (leftIndex > 0 && bracketIndex < 0)
                    {
                        bracketIndex = leftIndex;
                    }
                    else
                    {
                        bracketIndex = inputString.Length;
                    }
                    string outsideString = inputString.Substring(i, bracketIndex - i);
                        outsideString = outsideString.TrimEnd(',', ']');
                    arrayList.Add(outsideString.Split(',').Select(n => Convert.ToInt32(n)).ToArray());
                    i = bracketIndex-1;
                }
                i++;
            }
            return arrayList;
        }


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
            root.NodeInts = nodeInts.ToArray();
            return root;
        }

        public ICommand CheckCommand
        {
            get;
            private set;
        }
    }
}
