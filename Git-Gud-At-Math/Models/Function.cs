﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
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
        public List<List<Point3D>> FunctionSolutions3D { get; set; }
        public double Density => this.CalculateDensity();
        public bool IsSolutionFunction = false;

        public int FunctionId = 0;
        public string FunctionName = string.Empty;

        public Color FunctionColor { get; set; }
        public bool IsVisible { get; set; }

        // Static global next function id
        public static int NextFunctionId = 0;
        #endregion

        #region Constructors
        public Function()
        {
            this.FunctionSolutions = new List<Point>();
            this.FunctionSolutions3D = new List<List<Point3D>>();

            this.FunctionColor = (Color)ColorConverter.ConvertFromString("#" + ColorGenerator.GetColor.NextColour());
            this.IsVisible = true;

            this.FunctionId = NextFunctionId++;
            this.FunctionName = this.FunctionId.ToString();
        }

        public Function(List<Point> functionSolutions)
        {
            this.FunctionAsString = "Only Solution List";
            this.FunctionSolutions = functionSolutions;
            this.FunctionTree = new TreeNode("Solutions", ValueType.Unknown);

            this.FunctionColor = (Color)ColorConverter.ConvertFromString("#" + ColorGenerator.GetColor.NextColour());
            this.IsVisible = true;

            this.FunctionId = NextFunctionId++;
            this.FunctionName = this.FunctionId.ToString();
            this.IsSolutionFunction = true;
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
                {"x", "0"}
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
        
        public void Calculate3D(double startX, double endX, double startY, double endY, double density)
        {
            var variables = new Dictionary<string, string>
            {
                {"x", "0"},
                {"y", "0"},
            };
            
            try
            {
                this.FunctionSolutions3D = Calculator.EvaluateFunctionTreeBetween3D(this.FunctionTree, variables, startX, endX, startY, endY, density);
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
