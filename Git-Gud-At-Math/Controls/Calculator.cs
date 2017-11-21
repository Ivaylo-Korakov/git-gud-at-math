using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
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
            {"!",Factorial }
        };
        #endregion


        #region Methods / Logic
        public static double EvaluateFunctionTree(TreeNode startNodeOfTree, Dictionary<string, string> variables)
        {
            double result = 0;

            try
            {
                ReplaceVariables(startNodeOfTree, variables);
                Debug.PrintTree(startNodeOfTree);

                result = CalculateTree(startNodeOfTree);
                Debug.PrintTree(startNodeOfTree);
                Debug.OutPutAttention(result);
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

        public static List<Point> EvaluateFunctionTreeBetween(TreeNode startNodeOfTree, Dictionary<string, string> variables,string variableToCalculateFor, double startPoint, double endPoint, double density)
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
                TreeNode tempTree = startNodeOfTree.Clone();
                variables[variableToCalculateFor] = pointer.ToString();
                double result = EvaluateFunctionTree(tempTree, variables);
                points.Add(new Point(pointer,result));
                lastPointer = pointer;
            }

            if (lastPointer < endPoint)
            {
                TreeNode tempTree = startNodeOfTree.Clone();
                variables[variableToCalculateFor] = endPoint.ToString();
                double result = EvaluateFunctionTree(tempTree, variables);
                points.Add(new Point(endPoint, result));
            }

            return points;
        }

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
            catch (Exception)
            {
                throw new UnableToCalculateExpressions("Unable to calculate the parsed tree from the input string");
            }
        }

        public static double CalculateSimpleNodeTree(TreeNode node)
        {
            double result = 0;

            try
            {
                // Parse values into doubles
                List<double> values = new List<double>();
                foreach (var childNode in node.Children)
                {
                    values.Add(double.Parse(childNode.Value));
                }

                // Invoke a specific function
                if (Operations.ContainsKey(node.Value))
                {
                    result = Operations[node.Value](values);
                }
            }
            catch (Exception)
            {
                throw new UnableToCalculateExpressions("Unable to calculate the parsed tree from the input string");
            }

            return result;
        }

        public static void ReplaceVariables(TreeNode node, Dictionary<string, string> variables)
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
            catch (Exception )
            {
                throw new IncorrectNodeTreeFormat("The parsed tree is not correct! This error is when the input string is incorrect!");
            }
        }
        #endregion

        #region Math Operations
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
            var a = arguments.First();
            var b = arguments.Last();
            return a + b;
        }

        public static double Substraction(List<double> arguments)
        {
            var a = arguments.First();
            var b = arguments.Last();
            return a - b;
        }

        public static double Multiplication(List<double> arguments)
        {
            var a = arguments.First();
            var b = arguments.Last();
            return a * b;
        }

        public static double Devision(List<double> arguments)
        {
            var a = arguments.First();
            var b = arguments.Last();
            return a / b;
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
        #endregion
    }
}
