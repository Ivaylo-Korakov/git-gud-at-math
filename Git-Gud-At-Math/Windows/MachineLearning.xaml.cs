using System;
using System.Threading;
using System.Windows;
using Git_Gud_At_Math.Controls.MachineLearning;
using Git_Gud_At_Math.Utilities;
using LiveCharts;
using LiveCharts.Defaults;

namespace Git_Gud_At_Math.Windows
{
    /// <summary>
    /// Interaction logic for MachineLearning.xaml
    /// </summary>
    public partial class MachineLearning : Window
    {
        public ChartValues<ObservablePoint> TrainingDataSet { get; set; }
        public ChartValues<ObservablePoint> Hypothesis { get; set; }

        public double[,] CurrentTestData { get; set; }
      
        public MachineLearning()
        {
            InitializeComponent();
            // Open window center screen
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Assign data needed for the graph
            TrainingDataSet = new ChartValues<ObservablePoint>();
            Hypothesis = new ChartValues<ObservablePoint>();
            DataContext = this;

            // Add data to the list box
            this.TrainingDataList.Items.Add("Coursera Data");
            this.TrainingDataList.Items.Add("Test Data");
            this.TrainingDataList.Items.Add("Excel Data");
            this.TrainingDataList.Items.Add("Open Class Room Data");
        }

        private void DisplayData_Click(object sender, RoutedEventArgs e)
        {
            // Get selected
            if (this.TrainingDataList.SelectedItem == null)
            {
                MessageBox.Show("Please select a data set first!");
                return;
            }

            // Reset View
            this.TrainingDataSet.Clear();
            this.Hypothesis.Clear();

            // Assign training data
            switch (this.TrainingDataList.SelectedIndex)
            {
                case 0:
                    this.CurrentTestData = TrainingData.CourseraTs;
                    break;
                case 1:
                    this.CurrentTestData = TrainingData.TestDataTs;
                    break;
                case 2:
                    this.CurrentTestData = TrainingData.ExcelTs;
                    break;
                case 3:
                    this.CurrentTestData = TrainingData.OpenClassRoomTs;
                    break;
            }
            
            // Get training data
            for (int pointIndex = 0; pointIndex < CurrentTestData.GetLength(0); pointIndex++)
            {
                Point point = new Point(CurrentTestData[pointIndex, 0], CurrentTestData[pointIndex, 1]);
                TrainingDataSet.Add(new ObservablePoint(point.X, point.Y));
                Debug.OutPut(point);
            }
            
            // Get iterations
            int iterations;
            try
            {
                iterations = int.Parse(this.IterationsText.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a correct iteration int!");
                return;
            }

            // Run new thread with the gradient algorithm
            ThreadPool.QueueUserWorkItem(o =>
            {
                MachineLearningCalculator.GradientDescent(this.CurrentTestData, 0, 0, this.Hypothesis, iterations);
            });
        }
    }
}
