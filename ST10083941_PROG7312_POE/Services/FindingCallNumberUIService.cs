using ST10083941_PROG7312_POE.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ST10083941_PROG7312_POE.Services
{
    public class FindingCallNumberUIService
    {
        public static List<Button> Buttons;
        static List<Action> Questions = new() { LoadMidLevelQuestions, LoadBottomLevelQuestions};
        public static void LoadTopLevelQuestions()
        {
            ClearButtonTags();
            Questions = PopulateQuestions();
            FindingCallNumberService.PopulateTree();
            FindingCallNumberService.GetQuestion();
            var content = FindingCallNumberService.BottomNode.ToString().Split('-');
            FindingCallNumbers.TxtCallNumber.Text = content[1];
            Buttons.Shuffle();

            for (int i = 0; i < 4; i++)
            {
                if (FindingCallNumberService.TopLevelNodes[i].Equals(FindingCallNumberService.TopNode.ToString()))
                {
                    Buttons[i].Tag = "Answer";
                }
                Buttons[i].Content = FindingCallNumberService.TopLevelNodes[i];
                Buttons[i].Click += new RoutedEventHandler(OptionButtonClick);

            }
        }

        public static void LoadMidLevelQuestions()
        {
            Buttons.Shuffle();

            for (int i = 0; i < 4; i++)
            {
                if (FindingCallNumberService.MidLevelNodes[i].Equals(FindingCallNumberService.MiddleNode.ToString()))
                {
                    Buttons[i].Tag = "Answer";
                }

                Buttons[i].Content = FindingCallNumberService.MidLevelNodes[i];
                Buttons[i].Click += new RoutedEventHandler(OptionButtonClick);
            }
        }

        public static void LoadBottomLevelQuestions()
        {
            Buttons.Shuffle();

            for (int i = 0; i < 4; i++)
            {
                if (FindingCallNumberService.BottomLevelNodes[i].Equals(FindingCallNumberService.BottomNode.ToString()))
                {
                    Buttons[i].Tag = "Answer";
                }

                string content = FindingCallNumberService.BottomLevelNodes[i];
                var deweyPair = content.Split('-');
                Buttons[i].Content = deweyPair[0];
                Buttons[i].Click += new RoutedEventHandler(OptionButtonClick);
            }
        }

        public static void OptionButtonClick(object sender, RoutedEventArgs e)
        {
            Button? button = ((Button)sender);
            if (Questions.Count == 0 && button!.Tag != null)
            {
                MessageBox.Show("You have won! Congratulations!");
                ClearButtonTags();
                LoadTopLevelQuestions();
            }
            else if (button!.Tag != null)
            {
                ClearButtonTags();
                Questions[0]();
                Questions.RemoveAt(0);
            }
            else if (button!.Tag == null)
            {
                MessageBox.Show("Incorrect! Game has been reset");
                ClearButtonTags();
                LoadTopLevelQuestions();
            }
        }

        public static void ClearButtonTags()
        {
            foreach (var button in Buttons)
            {
                button.Tag = null;
                button.Click -= new RoutedEventHandler(OptionButtonClick);
               
            }
        }

        public static List<Action> PopulateQuestions()
        {
            return new List< Action> { LoadMidLevelQuestions, LoadBottomLevelQuestions };
        }
    }
}
