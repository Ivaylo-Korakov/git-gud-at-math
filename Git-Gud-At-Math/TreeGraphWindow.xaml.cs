using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Git_Gud_At_Math.Controls;
using Git_Gud_At_Math.Models;
using Debug = Git_Gud_At_Math.Utilities.Debug;

namespace Git_Gud_At_Math
{
    /// <summary>
    /// Interaction logic for TreeGraphWindow.xaml
    /// </summary>
    public partial class TreeGraphWindow : Window
    {
        public string FunctionNameAsString;
        public BitmapImage FunctionTreeImage;

        public TreeGraphWindow()
        {
            InitializeComponent();
        }

        public TreeGraphWindow(Function functionToGraph) : base()
        {
            InitializeComponent();

            Console.WriteLine(functionToGraph.FunctionAsString);
            this.FunctionNameAsString = "F(x) = " + functionToGraph.FunctionAsString;

            Debug.PrintTree(functionToGraph.FunctionTree);
            string treeAsString = Parser.ExportTree(functionToGraph.FunctionTree);
            Console.WriteLine(treeAsString);
            File.WriteAllText(functionToGraph.GetHashCode() + ".dot", treeAsString);
            Process dot = new Process();
            dot.StartInfo.FileName = @"C:\Program Files (x86)\Graphviz2.38\bin\dot.exe";
            dot.StartInfo.Arguments = "-Tpng -o"+ functionToGraph.GetHashCode() +".png " +functionToGraph.GetHashCode() + ".dot";
            dot.Start();
            dot.WaitForExit();

            this.FunctionTreeImage = new BitmapImage(new Uri(System.IO.Path.GetFullPath(functionToGraph.GetHashCode() + ".png")));
            this.Width = this.FunctionTreeImage.Width * 1.3;
            this.Height = this.FunctionTreeImage.Height * 1.5 + 50;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            this.FunctionName.Text = this.FunctionNameAsString;
            this.ImageOfTree.Source = this.FunctionTreeImage;
        }
    }
}
