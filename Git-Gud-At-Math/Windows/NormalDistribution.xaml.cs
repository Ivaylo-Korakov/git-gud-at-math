using Git_Gud_At_Math.Windows;
using System;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using Git_Gud_At_Math.Controls;
using System.Collections.Generic;

namespace Git_Gud_At_Math
{
    /// <summary>
    /// Interaction logic for NormalDistribution.xaml
    /// </summary>
    public partial class NormalDistribution : Window
    {
        public NormalDistribution()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            WindowManager.CloseWindow(this);
        }

        private void SimulateBtn_Click(object sender, RoutedEventArgs e)
        {
            // Init values
            int diceSides = 0;
            int dicePerGame = 0;
            int nOfGames = 0;

            // Get input
            try
            {
                diceSides = int.Parse(this.DiceSides.Text);
                dicePerGame = int.Parse(this.DicePerGame.Text);
                nOfGames = int.Parse(this.NumberOfGames.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter correct values");
                return;
            }

            // Create dice roller
            DiceRoller diceRoller = new DiceRoller(diceSides, dicePerGame, nOfGames);
        }

        private double CalculateMean(List<int> )
        {

        }
    }
}
