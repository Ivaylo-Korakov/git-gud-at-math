using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Git_Gud_At_Math.Controls;
using Git_Gud_At_Math.Controls.Views;
using Git_Gud_At_Math.Drawing;
using Git_Gud_At_Math.Models;
using Git_Gud_At_Math.Windows;

namespace Git_Gud_At_Math
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewController Controller { get; set; }
        public Painter Painter { get; set; }
        public FunctionEditor FunctionEditor { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(this.EverythingLoaded));
            // Open window center screen
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            this.Painter = new Painter(this);
            this.Controller = new MainViewController(this);
            this.FunctionEditor = new FunctionEditor(this);
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

        private void DeleteAllBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Controller.ClearFunctions();
            this.FunctionView.Items.Clear();
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
            //Console.WriteLine(this.InputStringFieldDensity.Text);
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

                var newFunction = DerivativeCalculator.CalculateDerivativeOf(this.Controller.CurrentSelectedFunction);

                // Create new function
                newFunction.FunctionName += " | D: " + this.Controller.CurrentSelectedFunction.FunctionId;

                // Add
                this.Controller.AddFunction(newFunction);
            }
        }

        private void DerivativeAnalyticalOrder_Click(object sender, RoutedEventArgs e)
        {
            string text = this.DerivativeOrderInput.Text;
            int orderOfDerivative = 1;

            if (int.TryParse(text, out orderOfDerivative) == false)
                return;

            if (this.Controller.CurrentSelectedFunction == null)
                return;

            if (this.Controller.CurrentSelectedFunction.IsSolutionFunction)
                return;

            // Get new derivative tree
            TreeNode newDerivativeTree =
                DerivativeCalculator.GetDerivativeOfN(this.Controller.CurrentSelectedFunction.FunctionTree.Clone(),
                    orderOfDerivative);

            // Create new function
            Function newFunction = new Function(newDerivativeTree);
            this.Controller.AddFunction(newFunction);
        }

        private void DerivativeNewtonBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.Controller.CurrentSelectedFunction != null)
            {
                if (this.Controller.CurrentSelectedFunction.IsSolutionFunction) return;
                double start = this.Painter.CanvasMinValue;
                double end = this.Painter.CanvasMaxValue;
                Function tempFunc = DerivativeCalculator.CalculateNewtonQuotient(
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
                //Console.WriteLine(answer);
            }
        }

        private void McLaurinBtn_Click(object sender, RoutedEventArgs e)
        {
            // Get and validate input
            string inputN = this.McLaurinN.Text;
            string inputA = this.McLaurinA.Text;
            int n = 0;
            int a = 0;

            if (int.TryParse(inputN, out n) == false)
            {
                MessageBox.Show("Enter a valid integer for N");
                return;
            }
            if (int.TryParse(inputA, out a) == false)
            {
                MessageBox.Show("Enter a valid integer for A");
                return;
            }

            if (n <= 0 || n > 8)
            {
                MessageBox.Show("N must be between 0 and 8");
                return;
            }

            if (this.Controller.CurrentSelectedFunction != null)
            {
                Function newMcSeriesFunction =
                    SeriesCalculator.CalculateMcLaurinSeries(Controller.CurrentSelectedFunction, a, n);
                
                this.Controller.AddFunction(newMcSeriesFunction);
            }
        }

        private void CreativeModeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.FunctionEditor.IsActive == false)
            {
                this.FunctionEditor.Enable();
            }
            else
            {
                this.FunctionEditor.GenerateFunction();
            }
        }

        private void CancelEditMode_Click(object sender, RoutedEventArgs e)
        {
            this.FunctionEditor.Disable();
        }

        private void Canvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point p = Mouse.GetPosition(this.Canvas);
            this.FunctionEditor.AddPoint(p);
        }

        private void slValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.Painter != null)
            {
                this.Painter.ChangeZoomLevel(e.NewValue);
            }
            //Console.WriteLine(e.NewValue);
        }

        private void Canvas_MouseWheel_1(object sender, MouseWheelEventArgs e)
        {
            double zoomSpeed = 2;
            if (e.Delta < 0)
            {
                this.slValue.Value -= zoomSpeed;
            }
            else
            {
                this.slValue.Value += zoomSpeed;
            }
        }

        private void NormalDistributionBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.OpenWindow(new NormalDistribution());
        }

        private void Function3D_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.OpenWindow(new FunctionView3D());
        }

        private void MachineLearning_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.OpenWindow(new MachineLearning());
        }
    }
}
