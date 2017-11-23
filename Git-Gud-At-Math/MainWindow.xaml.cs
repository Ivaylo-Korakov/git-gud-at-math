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
using System.Windows.Threading;
using Git_Gud_At_Math.Controls;
using Git_Gud_At_Math.Controls.Views;
using Git_Gud_At_Math.Drawing;
using Git_Gud_At_Math.Models;
using Git_Gud_At_Math.Utilities;
using ValueType = Git_Gud_At_Math.Models.ValueType;

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

            this.Painter = new Painter(this.Canvas);
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
            var density = double.Parse(this.InputStringFieldDensity.Text);
            Controller.Calculate(input, density);
        }

        private void EverythingLoaded()
        {
            this.Painter.ResetCanvas();
        }

        private void ShowTreeBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Parser.ExportTree(this.Controller.CurrentTreeFunction));
        }
    }
}
