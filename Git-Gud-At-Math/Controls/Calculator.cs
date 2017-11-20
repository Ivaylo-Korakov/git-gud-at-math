using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Git_Gud_At_Math.Models;
using Git_Gud_At_Math.Utilities;
using ValueType = Git_Gud_At_Math.Models.ValueType;

namespace Git_Gud_At_Math.Controls
{
    public static class Calculator
    {
        public static double EvaluateFunctionTree(TreeNode startNodeOfTree, Dictionary<string, string> variables)
        {
            double result = 0;

            ReplaceVariables(startNodeOfTree, variables);
            Debug.PrintTree(startNodeOfTree);

            result = CalculateTree(startNodeOfTree);
            Debug.PrintTree(startNodeOfTree);
            Console.WriteLine(result);

            return result;
        }

        public static double CalculateTree(TreeNode node)
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

                return 0;
            }
        }

        public static double CalculateSimpleNodeTree(TreeNode node)
        {
            double result = 0;
            // Parse
            List<double> values = new List<double>();
            foreach (var childNode in node.Children)
            {
                values.Add(double.Parse(childNode.Value));
            }

            switch (node.Value)
            {
                case "+":
                    result = values.First() + values.Last();
                    break;
                case "-":
                    result = values.First() - values.Last();
                    break;
                case "*":
                    result = values.First() * values.Last();
                    break;
                case "/":
                    result = values.First() / values.Last();
                    break;
                case "^": // Power
                    result = Math.Pow(values.First(), values.Last());
                    break;
                case "s": // Sin
                    result = Math.Sin(values.First());
                    break;
                case "c": // Cosine
                    result = Math.Cos(values.First());
                    break;
                case "e": // e to the power 
                    result = Math.Pow(Math.E,  values.First());
                    break;
                case "l": // natural log    
                    result = Math.Log(values.First(),Math.E);
                    break;
                case "n": // natural
                    result = values.First();
                    break;
                case "r": // rational
                    result = values.First();
                    break;
                case "!": // factorial
                    result = MathNet.Numerics.SpecialFunctions.Factorial((int) values.First());
                    break;
            }

            return result;
        }

        public static void ReplaceVariables(TreeNode node, Dictionary<string, string> variables)
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
    }
}
