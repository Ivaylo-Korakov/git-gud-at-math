using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Function newFuncToAdd = new Function(input);
            Controller.AddFunction(newFuncToAdd);
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
            Console.WriteLine("asd");
            this.Painter.ResetCanvas();
        }

        private void ShowTreeBtn_Click(object sender, RoutedEventArgs e)
        {
            var temp = this.FunctionView.SelectedItem;
            if (temp != null)
            {
                Console.WriteLine(Parser.ExportTree((temp as Function).FunctionTree));
            }
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

        }

        private void DerivativeNewtonBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
