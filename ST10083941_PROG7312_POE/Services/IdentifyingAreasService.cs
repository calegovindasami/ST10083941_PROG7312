using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10083941_PROG7312_POE.Services
{
    public class IdentifyingAreasService
    {
        private readonly Dictionary<string, string> Categories = new()
        {
            { "000", "General Knowledge" },
            { "100", "Philosophy & Psychology" },
            { "200", "Religion" },
            { "300", "Social Sciences" },
            { "400", "Languages" },
            { "500", "Science" },
            { "600", "Technology" },
            { "700", "Arts & Recreation" },
            { "800", "Literature" },
            { "900", "History & Geography" }
        };

        private readonly List<string> IncorrectCategories = new List<string>
        {
            "Electronics",
            "Clothing",
            "Home & Garden",
            "Books",
            "Toys",
            "Sports",
            "Food",
            "Automotive",
            "Health & Beauty",
            "Jewelry",
            "Furniture",
            "Travel",
            "Music",
            "Movies",
            "Tech Gadgets",
            "Pets",
            "Outdoor",
            "Fitness",
            "Office Supplies"
        };

        private List<string> CategoryKeys;

        public IdentifyingAreasService()
        {
            CategoryKeys = Categories.Keys.ToList();
        }

        //Gets 4 valid call numbers and 3 fake ones if game matches desc to call num.
        public ObservableCollection<string> GenerateCallNums(bool isMatchDescToCallNum)
        {
            ObservableCollection<string> callNums = new();
            List<int> generatedIndexes = new();
            Random random = new();
            int index;

            for (int i = 0; i < 4; i++)
            {
                //loop to ensure all random numbers are unique
                do
                {
                    index = random.Next(CategoryKeys.Count);
                }
                while (generatedIndexes.Contains(index));

                generatedIndexes.Add(index);
                callNums.Add(CategoryKeys[index]);
            }

            generatedIndexes.Clear();

            if (isMatchDescToCallNum)
            {
                for (int i = 0; i < 3; i++)
                {
                    do
                    {
                        index = random.Next(1, 10);
                    }
                    while (generatedIndexes.Contains(index));

                    generatedIndexes.Add(index);
                    callNums.Add((index * 100 + 1000).ToString());
                }
            }

            callNums.Shuffle();

            return callNums;
        }

        //Generates descriptions based on call numbers and additional if required
        public ObservableCollection<string> GenerateDescs(ObservableCollection<string> callNums, bool isMatchCallNumToDesc)
        {
            ObservableCollection<string> descs = new();

            foreach (var num in callNums)
            {
                string? desc;
                Categories.TryGetValue(num, out desc);

                if (!string.IsNullOrEmpty(desc))
                {
                    descs.Add(desc); 
                }
            }

            if (isMatchCallNumToDesc)
            {
                Random random = new();
                List<int> generatedIndexes = new();
                int index;
                for (int i = 0; i < 3; i++)
                {
                    do
                    {
                        index = random.Next(IncorrectCategories.Count);
                    }
                    while (generatedIndexes.Contains(index));

                    generatedIndexes.Add(index);
                    descs.Add(IncorrectCategories[index]);
                }
            }

            descs.Shuffle();

            return descs;
        }

        //Returns dictionary of correct answers from call nums
        public Dictionary<string, string> GetCorrectOrder(ObservableCollection<string> callNums)
        {
            var correctOrder = new Dictionary<string, string>();

            foreach (var num in callNums)
            {
                string? desc;
                Categories.TryGetValue(num, out desc);

                if (!string.IsNullOrEmpty(desc))
                {
                    correctOrder.Add(num, desc);
                }
            }

            return correctOrder;
        }

        //Checks if two dictionaries are equal
        public bool IsOrderCorrect(ObservableCollection<string> callNums, Dictionary<string, string> answers)
        {
            var correctOrder = GetCorrectOrder(callNums);
            return Enumerable.SequenceEqual(correctOrder, answers);
        }

        //Gets the number of correct answers from the answer list
        public int GetNumberOfCorrectAnswers(ObservableCollection<string> callNums, Dictionary<string, string> answers)
        {
            int numCorrect = 0;
            var correctOrder = GetCorrectOrder(callNums);
            var correctKeys = correctOrder.Keys.ToList();
            correctKeys.Sort();
            var answerKeys = answers.Keys.ToList();
            answerKeys.Sort();

            for (int i = 0; i < answers.Count; i++)
            {
                var correctPair = answers.ElementAt(i);
                var answerPair = correctOrder.ElementAt(i);
                if (correctPair.Key == answerPair.Key && correctPair.Value == answerPair.Value)
                {
                    numCorrect++;
                }
            }

            return numCorrect;
        }

    }
}
