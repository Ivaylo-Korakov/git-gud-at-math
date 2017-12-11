using System;
using System.Collections.Generic;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using Git_Gud_At_Math.Controls;
using Git_Gud_At_Math.Utilities;

namespace Git_Gud_At_Math.Models
{
    public class Function
    {
        #region Fields / Properties
        public string FunctionAsString { get; set; }
        public TreeNode FunctionTree { get; set; }
        public List<Point> FunctionSolutions { get; set; }
        public double Density => this.CalculateDensity();

        public int FunctionId = 0;
        public string FunctionName = "";

        public Color FunctionColor { get; set; }
        public bool IsVisible { get; set; }


        public static int NextFunctionId = 0;
        #endregion

        #region Constructors
        public Function()
        {
            this.FunctionSolutions = new List<Point>();

            this.FunctionColor = (Color)ColorConverter.ConvertFromString("#" + ColorGenerator.GetColor.NextColour());
            this.IsVisible = true;

            this.FunctionId = NextFunctionId++;
            this.FunctionName = this.FunctionId.ToString();
        }

        public Function(string functionAsString) : this()
        {
            functionAsString = functionAsString.Trim();
            functionAsString = functionAsString.ToLower();

            this.FunctionAsString = functionAsString;
            this.ParseIntoTree();
        }

        public Function(TreeNode functionTreeRoot) : this()
        {
            this.FunctionTree = functionTreeRoot;
            this.FunctionTree = TreeSimplifier.Simplify(this.FunctionTree);

            this.FunctionAsString = Parser.ParseTreeToString(this.FunctionTree);
        }
        #endregion


        private void ParseIntoTree()
        {
            this.FunctionTree = new TreeNode("Root", ValueType.Unknown);
            Parser.ParseStringToTree(this.FunctionAsString, this.FunctionTree);
            this.FunctionTree = TreeSimplifier.Simplify(this.FunctionTree);
        }

        public void Calculate(double startPosition, double endPosition, double density)
        {
            var variables = new Dictionary<string, string>
            {
                {"x", "0"},
                {"y", "5"},
            };

            try
            {
              this.FunctionSolutions = Calculator.EvaluateFunctionTreeBetween(this.FunctionTree, variables, "x", startPosition, endPosition, density);
              Debug.PrintTree(this.FunctionTree);
            }
            catch (Exception e)
            {
                Debug.OutPutError("Something went wrong! Please try again! \n \n" + e);
            }
        }

        private double CalculateDensity()
        {
            if (this.FunctionSolutions.Count >= 2)
            {
                return this.FunctionSolutions[1].X - this.FunctionSolutions[0].X;
            }

            return 0;
        }

        public override string ToString()
        {
            return this.FunctionName + " | f(x) = " + FunctionAsString;
        }
    }
}
