using System;
using System.Collections.Generic;
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

        private List<string> CategoryVals;

        public IdentifyingAreasService()
        {
            CategoryKeys = Categories.Keys.ToList();
            CategoryVals = Categories.Values.ToList();
        }

        public ObservableCollection<string> GenerateCallNums(bool isMatchDescToCallNum)
        {
            ObservableCollection<string> callNums = new();
            List<int> generatedIndexes = new();
            Random random = new();
            int index;

            for (int i = 0; i < 4; i++)
            {
                do
                {
                    index = random.Next(CategoryKeys.Count);
                }
                while (generatedIndexes.Contains(index));

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

                    callNums.Add((index * 100 + 1000).ToString());
                }
            }

            return callNums;
        }


    }
}
