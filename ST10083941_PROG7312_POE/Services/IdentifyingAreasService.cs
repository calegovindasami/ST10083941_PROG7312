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
        public Dictionary<string, string> Categories = new()
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

        List<string> IncorrectCategories = new List<string>
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

        List<string> CallNumbers;

        public IdentifyingAreasService()
        {
            CallNumbers = Categories.Keys.ToList();
        }

        public Tuple<ObservableCollection<string>, ObservableCollection<string>> GetCallNumberQuestion()
        {
            ObservableCollection<string> callNumbers = new();
            ObservableCollection<string> descriptions = new();

            List<int> generatedNumbers = new();
            Random random = new();
            int randomIndex;

            for (int i = 0; i < 4; i++)
            {
                do
                {
                    randomIndex = random.Next(CallNumbers.Count);
                    generatedNumbers.Add(randomIndex);

                    callNumbers.Add(CallNumbers[randomIndex]);
                    descriptions.Add(Categories[callNumbers[i]]);
                }
                while (generatedNumbers.Contains(randomIndex));
            }

            generatedNumbers.Clear();

            for (int i = 0; i < 3; i++)
            {
                do
                {
                    randomIndex = random.Next(IncorrectCategories.Count);
                    generatedNumbers.Add(randomIndex);

                    descriptions.Add(IncorrectCategories[randomIndex]);
                }
                while (generatedNumbers.Contains(randomIndex));
            }

            return new Tuple<ObservableCollection<string>, ObservableCollection<string>>(callNumbers, descriptions);
        }


    }
}
