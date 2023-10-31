using GongSolutions.Wpf.DragDrop;
using ST10083941_PROG7312_POE.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ST10083941_PROG7312_POE.Controls
{
    /// <summary>
    /// Interaction logic for IdentifyingAreas.xaml
    /// </summary>
    public partial class IdentifyingAreas : UserControl
    {
        IdentifyingAreasService idService = new IdentifyingAreasService();
        ObservableCollection<string> CallNumbers = new();
        ObservableCollection<string> Descriptions = new();

        bool isMatchDescToCall = false;

        public IdentifyingAreas()
        {
            InitializeComponent();
            LoadQuestions();
        }

        //Converts lists to dictionary
        private Dictionary<string, string> ConstructDictionary()
        {
            Dictionary<string, string> questions = new();
            for (int i = 0; i < 4; i++)
            {
                questions.Add(CallNumbers[i], Descriptions[i]);
            }

            return questions;
        }

        //Validates and checks if user entered correct answers
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> answers = ConstructDictionary();
            var isCorrect = idService.IsOrderCorrect(CallNumbers, answers);
            var numCorrect = idService.GetNumberOfCorrectAnswers(CallNumbers, answers);

            double percentage = (numCorrect / 4.0 * 100.0);

            pbArea.Value = percentage;

            if (isCorrect)
            {
                successfulSnackbarArea.MessageQueue!.Enqueue($"You have gotten all correct! Congrats!");
                LoadQuestions();
            }

        }

        //Resets game
        private void LoadQuestions()
        {
            CallNumbers = idService.GenerateCallNums(isMatchDescToCall);
            Descriptions = idService.GenerateDescs(CallNumbers, !isMatchDescToCall);
            isMatchDescToCall = !isMatchDescToCall;
            lsvCallNumberArea.ItemsSource = CallNumbers;
            lsvDescArea.ItemsSource = Descriptions;
            pbArea.Value = 0;
        }

        private void btnBegin_Click(object sender, RoutedEventArgs e)
        {
            LoadQuestions();
        }
    }
}
