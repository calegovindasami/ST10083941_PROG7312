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

            //GetTopLevelOptions();
            //GetMidLevelOptions();
            GetLevelOptions(Root, TopLevelNodes, TopNode);
            GetLevelOptions(TopNode, MidLevelNodes, MiddleNode);
            GetLevelOptions(MiddleNode, BottomLevelNodes, BottomNode);
        }

        public static void GetTopLevelOptions()
        {
            var rng = new Random();
            var topLevelLength = Root.Count();
            var generatedIndexes = new List<int>(); 

            var randomIndex = 0;

            TopLevelNodes.Add(TopNode.ToString());

            for (int i = 0; i < 3; i++)
            {
                do
                {
                    randomIndex = rng.Next(0, topLevelLength);
                }
                while (Root.GetChildByIndex(randomIndex) == TopNode || generatedIndexes.Contains(randomIndex));


                generatedIndexes.Add(randomIndex);
                TopLevelNodes.Add(Root.GetChildByIndex(randomIndex).ToString());
            }
        }

        public static void GetMidLevelOptions()
        {
            var rng = new Random();
            var midLevelLength = TopNode.Count();

            var generatedIndexes = new List<int>();

            var randomIndex = 0;

            MidLevelNodes.Add(MiddleNode.ToString());

            for (int i = 0; i < 3; i++)
            {
                do
                {
                    randomIndex = rng.Next(0, midLevelLength);
                }
                while (TopNode.GetChildByIndex(randomIndex) == MiddleNode || generatedIndexes.Contains(randomIndex));

                generatedIndexes.Add(randomIndex);
                MidLevelNodes.Add(TopNode.GetChildByIndex(randomIndex).ToString());
            }
        }

        public static void GetLevelOptions(TreeNode highestNode, ObservableCollection<string> levelNodes, TreeNode correctLevelNode)
        {
            var rng = new Random();
            var levelLength = highestNode.Count;

            var generatedIndexes = new List<int>();

            var index = 0;

            levelNodes.Add(correctLevelNode.ToString());

            for (int i = 0; i < 3; i++)
            {
                do
                {
                    index = rng.Next(levelLength);
                }
                while (highestNode.GetChildByIndex(index) == correctLevelNode || generatedIndexes.Contains(index));

                generatedIndexes.Add(index);
                levelNodes.Add(highestNode.GetChildByIndex(index).ToString());
            }

        }
    }
}
