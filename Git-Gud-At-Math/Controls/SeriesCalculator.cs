using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Git_Gud_At_Math.Models;
using Git_Gud_At_Math.Utilities;
using ValueType = Git_Gud_At_Math.Models.ValueType;

namespace Git_Gud_At_Math.Controls
{
    public static class SeriesCalculator
    {
        public static Function CalculateMcLaurinSeries(Function function, int a, int n)
        {
            TreeNode rootNode = new TreeNode("+", ValueType.Operator);


            for (int index = 0; index <= n; index++)
            {
                if (index == 0)
                {
                    var value = Calculator.EvaluateFunctionTree(function.FunctionTree.Clone(),
                        new Dictionary<string, string>
                        {
                            {"x", a.ToString()}
                        });

                    rootNode.Add(new TreeNode(value.ToString(), ValueType.Constant));
                    continue;
                }
                
                // Get new part
                TreeNode part = GetMcLaurinNode(function.FunctionTree.Clone(), a, index);
                
                rootNode.Add(part);
            }
            
            Function endResult = new Function(rootNode.Clone());
            return endResult;
        }

        public static TreeNode GetMcLaurinNode(TreeNode functionTree, int a, int nodeIndex)
        {
            TreeNode root = new TreeNode("/", ValueType.Operator);

            TreeNode nominator = new TreeNode("*", ValueType.Operator);

            // Calculate nominator
            // Get the value of the derivative of N
            var derivativeTree = DerivativeCalculator.GetDerivativeOfN(functionTree.Clone(), nodeIndex);
            var value = Calculator.EvaluateFunctionTree(derivativeTree.Clone(),
                new Dictionary<string, string>
                {
                    {"x", a.ToString()}
                });
            nominator.Add(new TreeNode(value.ToString(), ValueType.Constant));
            

            TreeNode power = new TreeNode("^", ValueType.Operator);

            TreeNode diff = new TreeNode("-", ValueType.Operator);

            if (a == 0)
            {
                diff = new TreeNode("x", ValueType.Variable);
            }
            else
            {
                diff.Add(new TreeNode("x", ValueType.Variable));
                diff.Add(new TreeNode(a.ToString(), ValueType.Constant));
            }

            power.Add(diff.Clone());
            power.Add(new TreeNode(nodeIndex.ToString(), ValueType.Constant));

            nominator.Add(power.Clone());

            double factorial = MathNet.Numerics.SpecialFunctions.Factorial(nodeIndex);
            TreeNode denominator = new TreeNode(factorial.ToString(), ValueType.Constant);

            root.Add(nominator.Clone());
            root.Add(denominator.Clone());
            
            
            return root;
        }

    }
}
