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

        //Moved construct dictionary method since that should of been in IdentifyingAreasService

        //Validates and checks if user entered correct answers
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> answers = idService.ConstructDictionary(CallNumbers, Descriptions);
            var isCorrect = idService.IsOrderCorrect(CallNumbers, answers);
            var numCorrect = idService.GetNumberOfCorrectAnswers(CallNumbers, answers);

            //Calculation is for the UI component, not part of gameplay logic. 
            double percentage = (numCorrect / 4.0 * 100.0);

            pbArea.Value = percentage;

            if (isCorrect)
            {
                successfulSnackbarArea.MessageQueue!.Enqueue($"You have gotten all correct! Congrats!");
                LoadQuestions();
            }

        }

        //Resets game - including UI
        //Cannot move this to another class since I'll have to pass in UI components which is a bit messy.
        //isMatchDescToCall is kept here so that it is only changed when the user begins a new game. Adding it to service would mean having to
        //tie in the UI to game logic since only on certain events it is reassigned.
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
