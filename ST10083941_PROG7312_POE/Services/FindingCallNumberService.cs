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
        public static TreeNode CorrectTopNode { get; set; }
        public static TreeNode CorrectParentNode { get; set; }
        public static TreeNode CorrectNode { get; set; }
        public static ObservableCollection<string> TopLevelNodes = new();
        public static ObservableCollection<string> MidLevelNodes = new();

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
            CorrectNode = Root.GetRandomChild();
            CorrectParentNode = CorrectNode.Parent!;
            CorrectTopNode = CorrectParentNode.Parent!;

            GetTopLevelOptions();
            GetMidLevelOptions();
        }

        public static void GetTopLevelOptions()
        {
            var rng = new Random();
            var topLevelLength = Root.Count();
            var generatedIndexes = new List<int>(); 

            var randomIndex = 0;

            TopLevelNodes.Add(CorrectTopNode.ToString());

            for (int i = 0; i < 3; i++)
            {
                do
                {
                    randomIndex = rng.Next(0, topLevelLength);
                }
                while (Root.GetChildByIndex(randomIndex) == CorrectParentNode.Parent || generatedIndexes.Contains(randomIndex));


                generatedIndexes.Add(randomIndex);
                TopLevelNodes.Add(Root.GetChildByIndex(randomIndex).ToString());
            }
        }

        public static void GetMidLevelOptions()
        {
            var rng = new Random();
            var midLevelLength = CorrectTopNode.Count();

            var generatedIndexes = new List<int>();

            var randomIndex = 0;

            MidLevelNodes.Add(CorrectParentNode.ToString());

            for (int i = 0; i < 3; i++)
            {
                do
                {
                    randomIndex = rng.Next(0, midLevelLength);
                }
                while (CorrectTopNode.GetChildByIndex(randomIndex) == CorrectParentNode || generatedIndexes.Contains(randomIndex));

                generatedIndexes.Add(randomIndex);
                MidLevelNodes.Add(CorrectTopNode.GetChildByIndex(randomIndex).ToString());
            }
        }
    }
}
