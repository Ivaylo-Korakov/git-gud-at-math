using System;
using System.Collections.Generic;
using System.Windows;
using Git_Gud_At_Math.Exceptions;
using Git_Gud_At_Math.Models;
using Git_Gud_At_Math.Utilities;
using ValueType = Git_Gud_At_Math.Models.ValueType;

namespace Git_Gud_At_Math.Controls.Views
{
    public class MainViewController
    {
        public MainWindow Window { get; private set; }
        public TreeNode CurrentTreeFunction { get; private set; }
        
        public delegate void NewCalcFunctionDel(List<Point> points);
        public event NewCalcFunctionDel NewFunctionCalculated;

        public MainViewController(MainWindow mainWindow)
        {
            this.Window = mainWindow;
            this.NewFunctionCalculated += this.Window.Painter.NewFunctionToDraw;

            // Test data
            // /(*(-(x,3),+(c(n(-73)),r(1.6))),e(!(5)))
            // /(-(+(x,1),*(x,3)),^(x,2))
            // *(^(x,2),!(5))
            // ^(x,2)
            // +(*(c(x),2),*(s(*(2,x)),c(*(60,x))))
        }

        public void Calculate(string input,double density)         
        {
            double startPosition = -25;
            double endPosition = 25;

            var variables = new Dictionary<string, string>
            {
                {"x", "0"},
                {"y", "10"},
            };

            //try
            //{
                this.CurrentTreeFunction = new TreeNode("Root", ValueType.Unknown);
                Parser.ParseStringToTree(input, this.CurrentTreeFunction);

                List<Point> points = Calculator.EvaluateFunctionTreeBetween(this.CurrentTreeFunction, variables, "x", startPosition, endPosition, density);

                NewFunctionCalculated(points);
                Debug.PrintTree(this.CurrentTreeFunction);
            //}
            //catch (Exception)
            //{
            //    Debug.OutPutError("Something went wrong! Please try again!");
            //}
        }
    }
}
