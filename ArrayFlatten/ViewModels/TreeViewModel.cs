using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using ArrayFlatten.Commands;
using ArrayFlatten.Models;

namespace ArrayFlatten.ViewModels
{
    internal class TreeViewModel : INotifyPropertyChanged
    {
        public TreeViewModel()
        {
            OutputArrayString = "Enter a nested array";
            Root = new TreeNode<int[]>();
            CheckCommand = new CheckTreeCommand(this);
             WelcomeDialogue = 
                "Welcome!\nTo use this application:\n" +
                "1. Enter a nested array using a space between elements\n" +
                "2. Once a valid nested array is entered, click the \"Flatten\" button\n" +
                "(Only [] brackets, positive & negative integers, and " +
                "whitespace allowed\n" +
                "3. Nested arrays are converted from text to a nested array" +
                " then \"treeified\"\n" +
                "4. The tree structure is used to give a flattened array\n" +
                "Please refer to sourcecode and documentation for details.\n" +
                "By: Hyuk Jin Kwon \n" +
                "hyukjink@sfu.ca";
        }

        public TreeNode<int[]> Root { get; set; }

        public string InputArrayString { get; set; }
        private string _welcomeDialogue;
            
        public string WelcomeDialogue
        {
            get
            {
                return _welcomeDialogue;
                
            }
            set
            {
                _welcomeDialogue = value;
                PropertyChanged(this, new PropertyChangedEventArgs("WelcomeDialogue"));
            }
        }
        private string _outputArrayString = string.Empty;
        public string OutputArrayString
        {
            get
            {
                return _outputArrayString;
            }
            set
            {
                _outputArrayString = value;
                PropertyChanged(this, new PropertyChangedEventArgs("OutputArrayString"));
            }
        }

        private string _errorString = string.Empty;
        public string ErrorString
        {
            get
            {
                return _errorString;
                
            }
            set
            {
                _errorString = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ErrorString"));
            }
        }

        private bool _isErrorStringVisible;
        public bool IsErrorStringVisible
        {
            get
            {
                return _isErrorStringVisible;
            }
            set
            {
                _isErrorStringVisible = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsErrorStringVisible"));
            }
        }
        public bool IsFlattenButtonEnabled
        {
            get
            {
                bool isEnabled = IsValidArrayString(InputArrayString);
                IsErrorStringVisible = !isEnabled;
                return IsValidArrayString(InputArrayString);
            }
        }

        private bool IsValidArrayString(string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString))
            {
                return false;
            }
            int i = 0;
            while (i < inputString.Length)
            {
                if (inputString[i] != '[' && inputString[i] != '-'
                    && inputString[i] != ' ' && !char.IsNumber(inputString[i]))
                {
                    ErrorString = "Invalid character entered";
                    return false;
                }
                if (inputString[i] == '[')
                {
                    int rightBracketIndex = GetRightBracketIndex(inputString, i);
                    if (rightBracketIndex == -1)
                    {
                        ErrorString = "Bracket is not closed";
                        return false;
                    }
                    string substring = inputString.Substring(i, rightBracketIndex - i + 1);
                    if (substring.Length < 3)
                    {
                        i = rightBracketIndex;
                    }
                    else
                    {
                        bool success = IsValidArrayString(inputString.Substring(i + 1, rightBracketIndex - i - 1));
                        if (!success)
                        {
                            ErrorString = "Bracket is not closed";
                            return false;
                        }
                        i = rightBracketIndex;
                    }
                }
                else if (inputString[i] == '-')
                {
                    if (i == inputString.Length - 1)
                    {
                        ErrorString = "Array cannot ended with '-'";
                        return false;
                    }
                    if (!char.IsNumber(inputString[i + 1]))
                    {
                        ErrorString = "'-' must be followed by a number";
                        return false;
                    }
                }
                else if (inputString[i] == ' ')
                {
                    if (i != inputString.Length - 1)
                    {
                        if (!char.IsNumber(inputString[i + 1]) && inputString[i + 1] != '['
                            && inputString[i+1] != '-')
                        {
                            ErrorString = "Please enter a valid character after \' \'";
                            return false;
                        }
                    }
                }
              
                i++;
            }
            return true;
        }

        public void DoFlattenButton(string inputString)
        {
            Array inputArray = Arrayify(inputString);
            Root = Treeify(inputArray);
            OutputArrayString = Root.ArrayToString();
        }

        private Array Arrayify(string inputString)
        {
            Array array = RecursiveArrayify(inputString).ToArray();
            return array;
        }

        private List<object> RecursiveArrayify(string inputString)
        {
            List<object> arrayList = new List<object>();
            int i = 0;
            while (i < inputString.Length)
            {
                if (inputString[i] == '[')
                {
                    int endBracketIndex = GetRightBracketIndex(inputString, i);
                    string insideString = inputString.Substring(i, endBracketIndex-i + 1);
                    if (insideString.Length > 2)
                    {
                        insideString = insideString.Substring(1, insideString.Length-2);
                        arrayList.Add(RecursiveArrayify(insideString).ToArray());
                        i = endBracketIndex;
                    }
                    else
                    {
                        arrayList.Add(new int[] {});
                    }
                }
                else if (char.IsNumber(inputString[i]) || inputString[i] == '-')
                {
                    string subString = inputString.Substring(i);
                    int bracketIndex = subString.IndexOfAny(new char[] {'[',']'});
                    if (bracketIndex < 0)
                    {
                        bracketIndex = subString.Length;
                    }
                    subString = subString.Substring(0, bracketIndex);
                    subString = subString.TrimEnd(' ');
                    IEnumerable<int> stringInts = subString.Split(' ').Select(n => Convert.ToInt32(n));
                    foreach (int num in stringInts)
                    {
                        arrayList.Add(num);
                    }
                    i = i + bracketIndex-1;
                }
                i++;
            }
            return arrayList;
        }

        private int GetRightBracketIndex(string inputString, int i)
        {
            Stack<char> bracketStack = new Stack<char>();
            if (inputString[i] != '[')
            {
                return -1;
            }
            bracketStack.Push(inputString[i]);
            int endBracketIndex = i + 1;
            while (bracketStack.Count > 0 && endBracketIndex < inputString.Length)
            {
                if (inputString[endBracketIndex] == '[')
                {
                    bracketStack.Push('[');
                }
                if (inputString[endBracketIndex] == ']')
                {
                    bracketStack.Pop();
                    if (bracketStack.Count == 0)
                    {
                        return endBracketIndex;;
                    }
                }
                endBracketIndex++;
            }
            if (endBracketIndex < inputString.Length -1)
            {
                return endBracketIndex;
            }
            return -1;
        }

        private TreeNode<int[]> Treeify(Array nestedArray)
        {
            TreeNode<int[]> root = new TreeNode<int[]>();
            foreach (var element in nestedArray)
            {
                if (element is int)
                {
                    root.AddChild((int)element);
                }
                else if (element is Array)
                {
                    root.AddChild(Treeify((Array)element));
                }
            }
            return root;
        }

        public ICommand CheckCommand
        {
            get;
            private set;
        }
        #region INotifyPropertyChanged 
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

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
