﻿using ST10083941_PROG7312_POE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for FindingCallNumbers.xaml
    /// </summary>
    public partial class FindingCallNumbers : UserControl
    {
        public List<Button> OptionButtons;
        public static TextBlock TxtCallNumber;
        public static ProgressBar PBCalNumber;

        public FindingCallNumbers()
        {
            InitializeComponent();
            OptionButtons = new()
            {
                btnOptionOne,
                btnOptionTwo,
                btnOptionThree,
                btnOptionFour,
            };

            FindingCallNumberUIService.Buttons = OptionButtons;
            TxtCallNumber = txtCallNumber;
            PBCalNumber = pbCallNumber;
            FindingCallNumberUIService.LoadTopLevelQuestions();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            FindingCallNumberUIService.LoadTopLevelQuestions();
            MessageBox.Show("Game has been reset.");
        }
    }
}
