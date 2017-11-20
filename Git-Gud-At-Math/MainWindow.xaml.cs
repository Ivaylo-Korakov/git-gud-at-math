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
using Git_Gud_At_Math.Controls;
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
        public MainWindow()
        {
            InitializeComponent();

            var variables = new Dictionary<string, string>
            {
                {"x", "1"},
                {"y", "4"}
            };


            string toParse = @"/(*(-(x,3),+(c(n(-73)),r(1.6))),e(!(5)))";

            TreeNode parsedTree = new TreeNode("Root", ValueType.Unknown);
            Parser.ParseStringToTree(toParse, parsedTree);

            Debug.OutPutAttention("TO PARSE: " + toParse);
            Debug.PrintTree(parsedTree);

            Calculator.EvaluateFunctionTree(parsedTree, variables);

            // ==============

            //string parse2 = @"-(+(x,1),*(x,3))";
            //TreeNode parsedTree2 = new TreeNode("Root", ValueType.Unknown);
            //Parser.ParseStringToTree(parse2, parsedTree2);

            //Calculator.EvaluateFunctionTree(parsedTree2, variables);
        }
    }
}
