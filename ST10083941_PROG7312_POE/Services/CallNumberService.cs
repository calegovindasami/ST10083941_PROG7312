using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10083941_PROG7312_POE.Services
{
    public class CallNumberService
    {
        private Random Random;

        public CallNumberService()
        {
            Random = new();
        }

        //Generates a classification number between 0 and 1000 and formats it so that it is always 3 digits.
        private string GenerateClassificationNumber()
        {
            int randomNumber = Random.Next(0, 1000);
            return randomNumber.ToString("D3");
        }

        //Randomly generates a character and converts the character array to a string which is returned.
        private string GenerateAuthor()
        {
            char[] author = new char[3];
            for (int i = 0; i < 3; i++)
            {
                author[i] = (char) Random.Next('A', 'Z' + 1);
            }
            return new string(author);
        }

        //Generates a single call number.
        public string GenerateCallNumber()
        {
            return $"{GenerateClassificationNumber()}.{GenerateClassificationNumber()} {GenerateAuthor()}";
        }

        //Generates an observable collection of 10 call numbers.
        public ObservableCollection<string> GenerateCallNumbers()
        {
            var callNumbers = new ObservableCollection<string>();
            for (int i = 0; i < 10; i++)
            {
                callNumbers.Add(GenerateCallNumber());
            }
            return callNumbers;
        }

        //Checks the original callNumbers list to sorted, and returns the result.
        public bool IsOrderingCorrect(List<string> callNumbers)
        {
            var orderedCallNumbers = callNumbers.Select(x => x).ToList();
            orderedCallNumbers.Sort();
            bool isCallNumbersEqual = Enumerable.SequenceEqual(orderedCallNumbers, callNumbers);
            return isCallNumbersEqual;
        }

        //Sorts the call numbers and returns it in ascending order.
        public ObservableCollection<string> GetCorrectlyOrderedCallNumbers(List<string> callNumbers)
        {
            var orderedCallNumbers = new ObservableCollection<string>();
            callNumbers.Sort();
            foreach (string number in callNumbers)
            {
                orderedCallNumbers.Add(number);
            }
            return orderedCallNumbers;
        }
    }
}
