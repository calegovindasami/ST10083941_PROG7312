using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10083941_PROG7312_POE.Model
{
    public class TreeNode : IEnumerable<TreeNode>
    {
        private readonly Dictionary<string, TreeNode> _children = new();
        public string Id { get; set; }
        public string? Value { get; set; }
        public TreeNode Parent { get; set; }

        public TreeNode(string id, string? value)
        {
            Id = id;
            Value = value;
        }

        public TreeNode GetChild(string id)
        {
            return _children[id];
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

        public int Count
        {
            get { return _children.Count; }
        }

        public static TreeNode BuildTree(string tree)
        {
            var lines = tree.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            var result = new TreeNode("Root", null);
            var childNodes = new List<TreeNode> { result };

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();
                var indent = line.Length - trimmedLine.Length;
                var deweyPair = trimmedLine.Split('-');
                var child = new TreeNode(deweyPair[0], deweyPair[1]);
                childNodes[indent].Add(child);

                if (indent + 1 < childNodes.Count)
                {
                    childNodes[indent + 1] = child;
                } 
                else
                {
                    childNodes.Add(child);
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
    }
}
