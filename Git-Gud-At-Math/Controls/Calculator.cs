﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Media3D;
using Git_Gud_At_Math.Exceptions;
using Git_Gud_At_Math.Models;
using Git_Gud_At_Math.Utilities;
using ValueType = Git_Gud_At_Math.Models.ValueType;

namespace Git_Gud_At_Math.Controls
{
    public static class Calculator
    {
        #region Static Variables
        /// <summary>
        /// This dictionary stores all the key characters 
        /// and their corresponding functions.
        /// That way when you want to add something new just 
        /// add the character and assign a function that takes a list of doubles
        /// and returns the result as double.
        /// </summary>
        public static Dictionary<string, Func<List<double>, double>> Operations = new Dictionary<string, Func<List<double>, double>>()
        {
            {"+",Addition },
            {"-",Substraction },
            {"*",Multiplication },
            {"/",Devision },
            {"^",Power },
            {"s",Sin },
            {"c",Cosine },
            {"e",NaturalToThePower },
            {"l",NaturalLog },
            {"n",NaturalNumber },
            {"r",RationalNumber },
            {"!",Factorial },
            {"p",Pi }
        };
        #endregion

        /// <summary>
        /// This method calculates the value of a function and given variables
        /// 1. Replaces the variables with their numeric values
        /// 2. Passes the variable free tree to the CalculateTree method which does
        ///    the heavy lifting of actually calculating the tree
        /// </summary>
        /// <param name="startNodeOfTree">The start of the tree</param>
        /// <param name="variables">The variables to evaluate for</param>
        /// <returns>The result of the function</returns>
        #region Methods / Logic
        public static double EvaluateFunctionTree(TreeNode startNodeOfTree, Dictionary<string, string> variables)
        {
            double result = 0;

            try
            {
                ReplaceVariables(startNodeOfTree, variables);
                Debug.PrintTree(startNodeOfTree);
            }
            catch (Exception)
            {
                Debug.OutPutError("Unable to parse tree variables! Check your string!");
                throw;
            }

            try
            {
                result = CalculateTree(startNodeOfTree);
                Debug.PrintTree(startNodeOfTree);
                Debug.OutPutAttention("For: x= " + variables["x"] + " y= " + result);

                if (double.IsNaN(result) || double.IsInfinity(result))
                {
                    throw new UnableToCalculateExpressions("NaN or infinity | UNDEFIEND");
                }
            }
            catch (UnableToCalculateExpressions e)
            {
                e.Print();
                throw;
            }
            catch (Exception)
            {
                Debug.OutPutError("Unable to calculate tree due to " +
                                  "unexpected circumstances. Check if " +
                                  "you string is correct and try again!");
                throw;
            }

            return result;
        }

        public static List<Point> EvaluateFunctionTreeBetween(TreeNode startNodeOfTree, Dictionary<string, string> variables, string variableToCalculateFor, double startPoint, double endPoint, double density)
        {
            List<Point> points = new List<Point>();

            density = Math.Abs(density);

            if (startPoint > endPoint)
            {
                double temp = startPoint;
                startPoint = endPoint;
                endPoint = temp;
            }

            double lastPointer = startPoint;
            for (double pointer = startPoint; pointer <= endPoint; pointer += density)
            {
                // Round pointer
                pointer = Math.Round(pointer, 4);
                try
                {
                    TreeNode tempTree = startNodeOfTree.Clone();
                    variables[variableToCalculateFor] = pointer.ToString();
                    double result = EvaluateFunctionTree(tempTree, variables);
                    CheckAndAddPoint(new Point(pointer, result), points);
                    lastPointer = pointer;
                }
                catch (UnableToCalculateExpressions) { continue; }
            }

            if (lastPointer < endPoint)
            {
                try
                {
                    TreeNode tempTree = startNodeOfTree.Clone();
                    variables[variableToCalculateFor] = endPoint.ToString();
                    double result = EvaluateFunctionTree(tempTree, variables);
                    CheckAndAddPoint(new Point(endPoint, result), points);
                }
                catch (UnableToCalculateExpressions) { }
            }

            return points;
        }

        public static List<List<Point3D>> EvaluateFunctionTreeBetween3D(TreeNode startNodeOfTree, Dictionary<string, string> variables, double startX, double endX, double startY, double endY, double density)
        {
            List<List<Point3D>> points = new List<List<Point3D>>();

            density = Math.Abs(density);

            if (startX > endX)
            {
                double temp = startX;
                startX = endX;
                endX = temp;
            }

            if (startY > endY)
            {
                double temp = startY;
                startY = endY;
                endY = temp;
            }

            for (double xIndex = startX; xIndex <= endX; xIndex += density)
            {
                List<Point3D> rowResults = new List<Point3D>();

                for (double yIndex = startY; yIndex <= endY; yIndex += density)
                {
                    TreeNode tempTree = startNodeOfTree.Clone();
                    variables["x"] = xIndex.ToString();
                    variables["y"] = yIndex.ToString();

                    try
                    {
                        double result = EvaluateFunctionTree(tempTree, variables);
                        rowResults.Add(new Point3D(xIndex, yIndex, result));
                    }
                    catch (Exception e)
                    {
                        rowResults.Add(new Point3D(xIndex, yIndex, 0));
                    }
                }

                points.Add(rowResults);
            }

            return points;
        }

        public static void CheckAndAddPoint(Point pointToAdd, List<Point> points)
        {
            if (double.IsNaN(pointToAdd.X) || double.IsNaN(pointToAdd.Y))
            {
                return;
            }

            if (double.IsInfinity(pointToAdd.X) || double.IsInfinity(pointToAdd.Y))
            {
                return;
            }

            if (pointToAdd.X >= double.PositiveInfinity || pointToAdd.Y >= double.PositiveInfinity)
            {
                return;
            }

            if (pointToAdd.X <= double.NegativeInfinity || pointToAdd.Y <= double.NegativeInfinity)
            {
                return;
            }

            Debug.OutPut("Adding: " + pointToAdd);
            points.Add(pointToAdd);
        }

        /// <summary>
        /// With the use of recursion this method goes down the tree structure
        /// until it reaches a simple tree (tree with to levels).
        /// When it goes to a simple tree it invokes the CalculateSimpleNodeTree function
        /// then gets the result of this simple tree and goes up again.
        /// Goes to the bottom and then step by step climbs back up to the root node
        /// </summary>
        /// <param name="node">The starting point of the tree (tree without variables)</param>
        /// <returns>The result from calculating the tree</returns>
        public static double CalculateTree(TreeNode node)
        {
            try
            {
                if (node.TypeOfValue == ValueType.Unknown)
                {
                    return CalculateTree(node.Children.First());
                }

                if (node.TypeOfValue == ValueType.Constant)
                {
                    if (Operations.ContainsKey(node.Value))
                    {
                        return CalculateSimpleNodeTree(node);
                    }
                    return double.Parse(node.Value);
                }
                else
                {
                    // Check
                    foreach (var nodeChild in node.Children)
                    {
                        if (nodeChild.TypeOfValue == ValueType.Operator)
                        {
                            CalculateTree(nodeChild);
                        }
                    }

                    // else
                    double result = CalculateSimpleNodeTree(node);
                    node.Value = result.ToString();
                    node.TypeOfValue = ValueType.Constant;

                    return result;
                }
            }
            catch (UnableToCalculateExpressions e)
            {
                throw;
            }
            catch (Exception)
            {
                string incorectString = node.Value;
                throw new UnableToCalculateExpressions("Unable to calculate the parsed tree from the input string", incorectString);
            }
        }

        /// <summary>
        /// This methods calculates a tree which has children but those children don't
        /// AKA 2 level tree
        /// Level 1:
        /// Operator node
        /// Level 2:
        /// Constant nodes
        /// </summary>
        /// <param name="node">The start node of the tree</param>
        /// <returns>The total value of the calculate tree</returns>
        public static double CalculateSimpleNodeTree(TreeNode node)
        {
            double result = 0;

            List<double> values = new List<double>();
            //try
            //{
            // Parse values into doubles
            foreach (var childNode in node.Children)
            {
                if (Operations.ContainsKey(childNode.Value))
                {
                    values.Add(Operations[childNode.Value](values));
                }
                else
                {
                    values.Add(double.Parse(childNode.Value));
                }
            }

            // Invoke a specific function
            if (Operations.ContainsKey(node.Value))
            {
                result = Operations[node.Value](values);
            }
            //}
            //catch (Exception)
            //{
            //    string incorectString = node.Value + " ( " + string.Join(",", node.Children) + " )";
            //    throw new UnableToCalculateExpressions("Unable to calculate the parsed tree from the input string", incorectString);
            //}

            // Remove double imprecisions with rounding 
            result = Math.Round(result, 4);
            return result;
        }

        /// <summary>
        /// With the use of recursion it goes down the tree and
        /// when it sees a node which has a type of value ==  variable
        /// it checks if it has a value for that variable and if so
        /// replaces the node value with a numeric value and sets the 
        /// type of value to a constant so it does not do it again when it sees the node
        /// </summary>
        /// <param name="node">The start node of the tree</param>
        /// <param name="variables">The variables and their values that you want to replace</param>
        private static void ReplaceVariables(TreeNode node, Dictionary<string, string> variables)
        {
            try
            {
                // Traverse the tree
                if (node.TypeOfValue == ValueType.Variable)
                {
                    if (variables.ContainsKey(node.Value))
                    {
                        // Replace
                        node.Value = variables[node.Value];
                        node.TypeOfValue = ValueType.Constant;
                    }
                }
                else
                {
                    foreach (var nodeChild in node.Children)
                    {
                        ReplaceVariables(nodeChild, variables);
                    }
                }
            }
            catch (Exception)
            {
                throw new IncorrectNodeTreeFormat("The parsed tree is not correct! This error is when the input string is incorrect!");
            }
        }

        #endregion

        /// <summary>
        /// This region contains all the basic mathematic functions 
        /// All of them have the same signature.
        /// If you need to add a basic math function you need to create 
        /// a new method here that does the operation before you add it
        /// to the operations dictionary
        /// </summary>
        /// <param name="arguments">The values needed to perform the operation</param>
        /// <returns>The result of the operation</returns>
        #region Math DerivativeRules
        public static double NaturalNumber(List<double> arguments)
        {
            return arguments.First();
        }

        public static double RationalNumber(List<double> arguments)
        {
            return arguments.First();
        }

        public static double Addition(List<double> arguments)
        {
            double result = 0;
            foreach (var arg in arguments)
            {
                result += arg;
            }
            return result;
        }

        public static double Substraction(List<double> arguments)
        {
            double result = arguments.First();
            int counter = 0;
            foreach (var arg in arguments)
            {
                if (counter == 0)
                {
                    counter++;
                    continue;
                }
                result -= arg;
            }
            return result;
        }

        public static double Multiplication(List<double> arguments)
        {
            double result = arguments[0];
            int counter = 0;
            foreach (var arg in arguments)
            {
                if (counter == 0)
                {
                    counter++;
                    continue;
                }
                result *= arg;
            }
            return result;
        }

        public static double Devision(List<double> arguments)
        {
            double result = arguments[0];
            int counter = 0;
            foreach (var arg in arguments)
            {
                if (counter == 0)
                {
                    counter++;
                    continue;
                }
                result /= arg;
            }
            return result;
        }

        public static double Sin(List<double> arguments)
        {
            var a = arguments.First();
            return Math.Sin(a);
        }

        public static double Cosine(List<double> arguments)
        {
            var a = arguments.First();
            return Math.Cos(a);
        }

        public static double Power(List<double> arguments)
        {
            var a = arguments.First();
            var b = arguments.Last();
            return Math.Pow(a, b);
        }

        public static double NaturalToThePower(List<double> arguments)
        {
            var a = arguments.First();
            return Math.Pow(Math.E, a);
        }

        public static double NaturalLog(List<double> arguments)
        {
            var a = arguments.First();
            return Math.Log(a, Math.E);
        }

        public static double Factorial(List<double> arguments)
        {
            var a = arguments.First();
            return MathNet.Numerics.SpecialFunctions.Factorial((int)a);
        }

        public static double Pi(List<double> arguments)
        {
            return Math.PI;
        }
        #endregion
    }
}
