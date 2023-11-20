using ST10083941_PROG7312_POE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10083941_PROG7312_POE.Services
{
    public class FindingCallNumberService
    {
        public TreeNode Root = TreeNode.BuildTree(@"
500-Natural Science and Mathematics
 510-Mathematics
  511-General principles of mathematics
  512-Algebra
  513-Arithmetic
  514-Topology
400-Language
 410-Linguistics
  411-Writing systems of standard forms of languages
  412-Etymology of standard forms of languages
  413-Dictionaries of standard forms of languages
  414-Phonology and phonetics of standard forms of languages");

        public TreeNode CorrectParentNode;

        public TreeNode GetThirdLevelEntry()
        {
            TreeNode randomNode = Root.GetRandomChild();
            CorrectParentNode = randomNode;
            return randomNode.GetRandomChild().GetRandomChild();
        }

        public List<TreeNode> GetFourTopLevelEntries()
        {
            List<TreeNode> nodes = new()
            {
                CorrectParentNode
            };
            TreeNode incorrectNode;

            for (int i = 0; i < 3; i++)
            {
                do
                {
                    incorrectNode = Root.GetRandomChild();
                } while (CorrectParentNode != incorrectNode);

                nodes.Add(incorrectNode);
            }

            return nodes;

        }
    }
}
