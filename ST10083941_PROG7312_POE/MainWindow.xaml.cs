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

namespace ST10083941_PROG7312_POE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> CallNumbers;
        private CallNumberService CallNumberService;
        private Stopwatch Stopwatch;
        private Timer Timer;
        private string StartTime = "00:00:00";

        public MainWindow()
        {
            InitializeComponent();

            CallNumberService = new();
            CallNumbers = new();
            Stopwatch = new();
            Timer = new(1000);
            Timer.Elapsed += OnTimerElapse!;

            LoadCallNumbers();
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            var currentIndex = lsvCallNumbers.SelectedIndex;
            if (currentIndex > 0)
            {
                var currentCallNumber = CallNumbers[currentIndex];
                CallNumbers.RemoveAt(currentIndex);
                CallNumbers.Insert(currentIndex - 1, currentCallNumber);
                lsvCallNumbers.SelectedIndex = currentIndex - 1;
            }
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            var currentIndex = lsvCallNumbers.SelectedIndex;
            if (currentIndex + 1 < lsvCallNumbers.Items.Count)
            {
                var currentCallNumber = CallNumbers[currentIndex];
                CallNumbers.RemoveAt(currentIndex);
                CallNumbers.Insert(currentIndex + 1, currentCallNumber);
                lsvCallNumbers.SelectedIndex = currentIndex + 1;
            }
        }

        private void btnReplacingBooksSubmit_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch.Stop();
            Timer.Stop();
            var isOrderingCorrect = CallNumberService.IsOrderingCorrect(CallNumbers.ToList());
            if (isOrderingCorrect)
            {
                successfulSnackbar.MessageQueue!.Enqueue($"You have won! It took you {Stopwatch.Elapsed.ToString(@"hh\:mm\:ss")}");
                LeaderboardService.Add(Stopwatch.Elapsed);
            }
            else
            {
                unsuccessfulSnackbar.MessageQueue!.Enqueue("You have lost! Please view the correctly ordered list above.");
                CallNumbers = CallNumberService.GetCorrectlyOrderedCallNumbers(CallNumbers.ToList());
                lsvCallNumbers.ItemsSource = CallNumbers;
            }

            btnReplacingBooksBegin.IsEnabled = true;
            btnReplacingBooksSubmit.IsEnabled = false;
        }

        private void OnTimerElapse(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => tblTimer.Text = Stopwatch.Elapsed.ToString(@"hh\:mm\:ss"));
        }
        private void BeginTimer()
        {
            Stopwatch.Reset();
            Timer.Stop();
            tblTimer.Text = StartTime;

            Stopwatch.Start();
            Timer.Start();

            LoadCallNumbers();

            btnReplacingBooksBegin.IsEnabled = false;
            btnReplacingBooksSubmit.IsEnabled = true;
        }

        private void btnReplacingBooksBegin_Click(object sender, RoutedEventArgs e)
        {
            BeginTimer();
        }

        private void LoadCallNumbers()
        {
            CallNumbers = CallNumberService.GenerateCallNumbers();
            lsvCallNumbers.ItemsSource = CallNumbers;
            lsvCallNumbers.SelectedIndex = 0;
        }
    }
}
