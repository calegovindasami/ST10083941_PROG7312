
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10083941_PROG7312_POE.Model
{
    //The following code was taken from stackoverflow
    //Link: https://stackoverflow.com/questions/9860207/build-a-simple-high-performance-tree-data-structure-in-c-sharp
    //Author: Marcus Mangelsdorf
    public class TreeNode : IEnumerable<TreeNode>
    {
        private readonly Dictionary<string, TreeNode> _children = new();
        public string Id { get; set; }
        public string? Value { get; set; }
        public TreeNode? Parent { get; set; }
        public int Count { get { return _children.Count; } }

        public TreeNode(string id, string? value)
        {
            Id = id;
            Value = value;
        }

        public TreeNode GetChild(string id)
        {
            return _children[id];
        }

        public TreeNode GetChildByIndex(int index)
        {
            return _children.ElementAt(index).Value;
        }

        public ObservableCollection<string> GetChildren()
        {
            var children = new ObservableCollection<string>();
            foreach (var item in _children)
            {
                children.Add(item.Key + "-" + item.Value);
            }
            return children;
        }
        public TreeNode GetRandomChild()
        {
            var rng = new Random();
            var topLevel =  _children.ElementAt(rng.Next(Count)).Value;
            var midLevel = topLevel._children.ElementAt(rng.Next(topLevel.Count)).Value;
            var bottomLevel = midLevel._children.ElementAt(rng.Next(midLevel.Count)).Value;
            return bottomLevel;
        }

        public void Add(TreeNode item)
        {
            if (item.Parent != null)
            {
                item.Parent._children.Remove(item.Id);
            }

            item.Parent = this;
            _children.Add(item.Id, item);
        }

        public static TreeNode BuildTree(string tree)
        {
            var lines = tree.Split(new[] { Environment.NewLine },
                                   StringSplitOptions.RemoveEmptyEntries);

            var result = new TreeNode("root", null);
            var list = new List<TreeNode> { result };

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();
                var indent = line.Length - trimmedLine.Length;
                var deweyPair = trimmedLine.Split('-');
                var child = new TreeNode(deweyPair[0], deweyPair[1]);
                list[indent].Add(child);

                if (indent + 1 < list.Count)
                {
                    list[indent + 1] = child;
                }
                else
                {
                    list.Add(child);
                }
            }

            return result;
        }

        public IEnumerator<TreeNode> GetEnumerator()
        {
            return _children.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return Id + " - " + Value;
        }
    }
}
