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

        public TreeNode GetRandomChild()
        {
            var rnd = new Random();
            int number = rnd.Next(0, _children.Count);
            return _children.ElementAt(number).Value;
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
    }
}
