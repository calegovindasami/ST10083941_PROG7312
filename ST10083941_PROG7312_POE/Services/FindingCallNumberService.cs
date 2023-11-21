using ST10083941_PROG7312_POE.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10083941_PROG7312_POE.Services
{
    public class FindingCallNumberService
    {
        public static TreeNode Root { get; set; }
        public static TreeNode TopNode { get; set; }
        public static TreeNode MiddleNode { get; set; }
        public static TreeNode BottomNode { get; set; }
        public static ObservableCollection<string> TopLevelNodes = new();
        public static ObservableCollection<string> MidLevelNodes = new();
        public static ObservableCollection<string> BottomLevelNodes = new();

        public static void PopulateTree()
        {
            try
            {
                StreamReader reader = new("C:\\Users\\caleg\\Documents\\GitHub\\ST10083941_PROG7312\\ST10083941_PROG7312_POE\\DeweyDecimal.txt");
                string lines = reader.ReadToEnd();
                Root = TreeNode.BuildTree(lines);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        public static void GetQuestion()
        {
            BottomNode = Root.GetRandomChild();
            MiddleNode = BottomNode.Parent!;
            TopNode = MiddleNode.Parent!;

            TopLevelNodes.Clear();
            MidLevelNodes.Clear();
            BottomLevelNodes.Clear();

            GetLevelOptions(Root, TopLevelNodes, TopNode);
            GetLevelOptions(TopNode, MidLevelNodes, MiddleNode);
            GetLevelOptions(MiddleNode, BottomLevelNodes, BottomNode);
        }

        public static void GetLevelOptions(TreeNode highestNode, ObservableCollection<string> currentLevelNodes, TreeNode currentLevelNode)
        {
            var rng = new Random();
            var levelLength = highestNode.Count;

            var generatedIndexes = new List<int>();

            var index = 0;

            currentLevelNodes.Add(currentLevelNode.ToString());

            for (int i = 0; i < 3; i++)
            {
                do
                {
                    index = rng.Next(levelLength);
                }
                while (highestNode.GetChildByIndex(index) == currentLevelNode || generatedIndexes.Contains(index));

                generatedIndexes.Add(index);
                currentLevelNodes.Add(highestNode.GetChildByIndex(index).ToString());
            }

            var currentLevelNodesList = currentLevelNodes.ToList();
            currentLevelNodesList.Sort();
            currentLevelNodes.Clear();
            foreach (var item in currentLevelNodesList)
            {
                currentLevelNodes.Add(item);
            }
        }

        public static bool isLevelCorrect(TreeNode levelNode, string answer)
        {
            return levelNode.ToString().Equals(answer);
        }
    }
}
