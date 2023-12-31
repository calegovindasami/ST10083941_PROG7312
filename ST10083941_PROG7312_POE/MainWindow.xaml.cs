﻿using ST10083941_PROG7312_POE.Model;
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
        //Observable collection was used instead of a list so that the binding to the listView is automatic.
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
            lsvLeaderboard.ItemsSource = LeaderboardService.Get();
        }

        //Updates the list to move the selected item up
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

        //Updates the list to move the selected item down
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

        //Checks if the selected ordering is correct and displays the result to the user.
        private void btnReplacingBooksSubmit_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch.Stop();
            Timer.Stop();
            var isOrderingCorrect = CallNumberService.IsOrderingCorrect(CallNumbers.ToList());
            if (isOrderingCorrect)
            {
                successfulSnackbar.MessageQueue!.Enqueue($"You have won! It took you {Stopwatch.Elapsed.ToString(@"hh\:mm\:ss")}");
                LeaderboardService.Add(Stopwatch.Elapsed);
                var leaderboard = LeaderboardService.Get();
                lsvLeaderboard.ItemsSource = leaderboard;
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

        //Code Attribution
        //The following method was taken from this YouTube tutorial.
        //Author: James Tays
        //Website: YouTube
        //Link: https://www.youtube.com/watch?v=biag31sLlFM
        private void OnTimerElapse(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => tblTimer.Text = Stopwatch.Elapsed.ToString(@"hh\:mm\:ss"));
        }
        //End of Code Attribution

        //Resets and begins the timer
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

        //Generates random call numbers and sets the source and selected index of the listview.
        private void LoadCallNumbers()
        {
            CallNumbers = CallNumberService.GenerateCallNumbers();
            lsvCallNumbers.ItemsSource = CallNumbers;
            lsvCallNumbers.SelectedIndex = 0;
        }

        //Toggles views of the grids that contain the leaderboard and actual task.
        private void btnReplacingBooksLeaderboard_Click(object sender, RoutedEventArgs e)
        {
            gdLeaderboard.Visibility = Visibility.Visible;
            gdReplacingBooks.Visibility = Visibility.Collapsed;
        }

        private void btnLeaderboardBack_Click(object sender, RoutedEventArgs e)
        {
            gdLeaderboard.Visibility = Visibility.Collapsed;
            gdReplacingBooks.Visibility = Visibility.Visible;
        }
    }
}
