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
        private string GenerateClassificationNumber()
        {
            int randomNumber = Random.Next(0, 1000);
            return randomNumber.ToString("D3");
        }
        private string GenerateAuthor()
        {
            char[] author = new char[3];
            for (int i = 0; i < 3; i++)
            {
                author[i] = (char) Random.Next('A', 'Z' + 1);
            }
            return new string(author);
        }
        private string GenerateSpecificationNumber()
        {
            return Random.Next(0, 1000).ToString("D3");
        }


        public string GenerateCallNumber()
        {
            return $"{GenerateClassificationNumber()}.{GenerateSpecificationNumber()} {GenerateAuthor()}";
        }
        public ObservableCollection<string> GenerateCallNumbers()
        {
            var callNumbers = new ObservableCollection<string>();
            for (int i = 0; i < 10; i++)
            {
                callNumbers.Add(GenerateCallNumber());
            }
            return callNumbers;
        }

        public bool IsOrderingCorrect(List<string> callNumbers)
        {
            var orderedCallNumbers = callNumbers.Select(x => x).ToList();
            orderedCallNumbers.Sort();
            bool isCallNumbersEqual = Enumerable.SequenceEqual(orderedCallNumbers, callNumbers);
            return isCallNumbersEqual;
        }

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
