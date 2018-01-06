using Git_Gud_At_Math.Windows;
using System;
using System.Linq;
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

            Dictionary<double, int> meanData = new Dictionary<double, int>();
            List<List<int>> data = diceRoller.RollGames();

            foreach (List<int> item in data)
            {
                // Calculate mean
                double meanValue = this.CalculateMean(item);

                if (meanData.ContainsKey(meanValue))
                {
                    meanData[meanValue]++;
                }
                else
                {
                    meanData.Add(meanValue, 1);
                }
            }

            // Get labels
            List<double> labels = meanData.Keys.ToList();
            List<int> values = new List<int>();
            labels.Sort();

            // Display results
            this.ResultChart.Labels = labels.Select(a => a.ToString()).ToArray();
            this.LineResultChart.Labels = labels.Select(a => a.ToString()).ToArray();

            foreach (var item in labels)
            {
                values.Add(meanData[item]);
            }

            ChartValues<int> chartValues = new ChartValues<int>();
            chartValues.AddRange(values);

            this.ResultChart.SeriesCollection.Add(new ColumnSeries
            {
                Values = chartValues
            });

            this.LineResultChart.SeriesCollection.Add(new LineSeries
            {
                Values = chartValues
            });
        }

        private double CalculateMean(List<int> numbers)
        {
            double start = 0;
            foreach (var item in numbers)
            {
                start += item;
            }
            return start / numbers.Count;
        }

        private void ClearDataBtn_Click(object sender, RoutedEventArgs e)
        {
            this.ResultChart.Reset();
            this.LineResultChart.Reset();
        }
    }
}
