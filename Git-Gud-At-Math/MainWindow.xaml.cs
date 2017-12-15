using System;
using System.Collections.Generic;
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
                Controller.RemoveFunction(temp as Function);
                this.FunctionView.Items.Remove(temp);
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
            this.SelectedFuncLable.Text = Controller.CurrentSelectedFunction?.ToString() ?? "No function selected";
        }

        private void DerivativeAnalyticalBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.Controller.CurrentSelectedFunction != null)
            {
                if (this.Controller.CurrentSelectedFunction.IsSolutionFunction) return;
                // Get Derivative tree
                var tempTree =
                    DerivativeCalculator.GetDerivativeOfTree(this.Controller.CurrentSelectedFunction.FunctionTree);

                // Create new function
                Function tempFunc = new Function(tempTree);
                tempFunc.FunctionName += " | D: " + this.Controller.CurrentSelectedFunction.FunctionId;

                // Add
                this.Controller.AddFunction(tempFunc);
            }
        }

        private void DerivativeNewtonBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.Controller.CurrentSelectedFunction != null)
            {
                if (this.Controller.CurrentSelectedFunction.IsSolutionFunction) return;
                double start = this.Painter.CanvasMinValue;
                double end = this.Painter.CanvasMaxValue;
                Function tempFunc =  DerivativeCalculator.CalculateNewtonQuotient(
                    this.Controller.CurrentSelectedFunction,
                    new Dictionary<string, string>()
                    {
                        {"x", "0"}
                    },
                    "x", start, end, 0.1);

                // Create new function
                tempFunc.FunctionName += " | D: " + this.Controller.CurrentSelectedFunction.FunctionId;

                // Add
                this.Controller.AddSolutionFunction(tempFunc);
            }
        }

        private void CalcIntegralBtn_Click(object sender, RoutedEventArgs e)
        {
            double start = double.Parse(this.IntegralStart.Text);
            double end = double.Parse(this.IntegralEnd.Text);

            if (this.Controller.CurrentSelectedFunction != null)
            {
                if (this.Controller.CurrentSelectedFunction.IsSolutionFunction) return;
                double answer = IntegralCalculator.CalculateIntegralOf(this.Controller.CurrentSelectedFunction, start, end);
                this.CalculatedIntegral.Content = answer;
                Console.WriteLine(answer);
            }
        }
    }
}
