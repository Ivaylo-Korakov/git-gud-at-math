using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Git_Gud_At_Math.Controls;
using Git_Gud_At_Math.Controls.Views;
using Git_Gud_At_Math.Drawing;
using Git_Gud_At_Math.Models;

namespace Git_Gud_At_Math
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewController Controller { get; set; }
        public Painter Painter { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(this.EverythingLoaded));

            this.Painter = new Painter(this);
            this.Controller = new MainViewController(this);
        }

        /// <summary>
        /// EVENT:
        /// This method is called when the button is pressed
        /// Takes the input from the user and sends it to the controller to calculate it
        /// </summary>
        private void SolveBtn_Click(object sender, RoutedEventArgs e)
        {
            var input = this.InputStringField.Text;
            if (input.Length >= 1)
            {
                Function newFuncToAdd = new Function(input);
                Controller.AddFunction(newFuncToAdd);
                this.InputStringField.Text = String.Empty;
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var temp = this.FunctionView.SelectedItem;
            if (temp != null)
            {
                this.FunctionView.Items.Remove(temp);
                Controller.RemoveFunction(temp as Function);
            }
        }

        private void EverythingLoaded()
        {
            this.Painter.ResetCanvas();
        }

        private void ShowTreeBtn_Click(object sender, RoutedEventArgs e)
        {
            Controller.ShowFunctionTree();
        }

        private void InputStringFieldDensity_TextChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine(this.InputStringFieldDensity.Text);
        }

        private void FunctionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = this.FunctionView.SelectedItem;
            if (temp != null)
            {
                this.Controller.CurrentSelectedFunction = (temp as Function);
            }
            this.SelectedFuncLable.Text = this.Controller.CurrentSelectedFunction.ToString();
        }

        private void DerivativeAnalyticalBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.Controller.CurrentSelectedFunction != null)
            {
                // Get Derivative tree
                var tempTree =
                    DerivativeCalculator.GetDerivativeOfTree(this.Controller.CurrentSelectedFunction.FunctionTree);

                // Create new function
                Function tempFunc = new Function(tempTree);

                // Add
                this.Controller.AddFunction(tempFunc);
            }
        }

        private void DerivativeNewtonBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CalcIntegralBtn_Click(object sender, RoutedEventArgs e)
        {
            double start = double.Parse(this.IntegralStart.Text);
            double end = double.Parse(this.IntegralEnd.Text);

            if (this.Controller.CurrentSelectedFunction != null)
            {
                double answer = IntegralCalculator.CalculateIntegralOf(this.Controller.CurrentSelectedFunction, start, end);
                this.CalculatedIntegral.Content = answer;
                Console.WriteLine(answer);
            }
        }
    }
}
