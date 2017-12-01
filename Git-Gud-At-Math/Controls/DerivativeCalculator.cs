using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Git_Gud_At_Math.Models;
using ValueType = Git_Gud_At_Math.Models.ValueType;

namespace Git_Gud_At_Math.Controls
{
    public static class DerivativeCalculator
    {
        public static Dictionary<string, Func<List<TreeNode>, TreeNode>> DerivativeRules = new Dictionary<string, Func<List<TreeNode>, TreeNode>>()
        {
            {"+",SumRule },
            {"-",DifferenceRule },
            {"*",ProductRule },
            {"/",QuotientRule },
            {"^",PowerRule }
        };

        private static TreeNode PowerRule(List<TreeNode> arg)
        {
            throw new NotImplementedException();
        }


        public static Function CalculateDerivativeOf(Function function)
        {
            TreeNode derivativeTree = GetDerivativeOfTree(function.FunctionTree.Clone());

            Function derivativeFunction = new Function(derivativeTree);

            return derivativeFunction;
        }

        public static TreeNode GetDerivativeOfTree(TreeNode startNode)
        {
            if (startNode.TypeOfValue == ValueType.Operator)
            {
                bool isChildAnOperator = false;
                // Check if there is operator in children
                foreach (var startNodeChild in startNode.Children)
                {
                    if (startNodeChild.TypeOfValue == ValueType.Operator)
                    {
                        isChildAnOperator = true;
                        GetDerivativeOfTree(startNodeChild);
                    }
                }

                // Else - Simple Tree
                if (isChildAnOperator == false)
                {
                    var newDerivedTree = GetDerivativeOfSimpleTree(startNode.Clone());
                    newDerivedTree.Parent = startNode.Parent;
                    startNode.Parent.Children.Add(newDerivedTree);
                    startNode.Parent.Children.Remove(startNode);
                }
            }

            return null;
        }

        public static TreeNode GetDerivativeOfSimpleTree(TreeNode startNode)
        {
            if (DerivativeRules.ContainsKey(startNode.Value))
            {
               return DerivativeRules[startNode.Value](startNode.Children);
            }
            else
            {
                throw new Exception("There is no know rule for this operator!  Simple tree");
            }
        }

        #region DerivativeRules
        private static TreeNode QuotientRule(List<TreeNode> arg)
        {
            TreeNode quotientNode = new TreeNode("/", ValueType.Operator);

            // Add f`g - fg`Numerator
            TreeNode numeratorNode = new TreeNode("-", ValueType.Operator);

            TreeNode leftMultiply = new TreeNode("*", ValueType.Operator);
            TreeNode rightMultiply = new TreeNode("*", ValueType.Operator);

            if (arg.First().TypeOfValue == ValueType.Constant) // F
            {
                leftMultiply.Add(new TreeNode("0", ValueType.Constant));
                rightMultiply.Add(new TreeNode(arg.First().Value, ValueType.Constant));
            }
            if (arg.Last().TypeOfValue == ValueType.Constant) // G
            {
                rightMultiply.Add(new TreeNode("0", ValueType.Constant));
                leftMultiply.Add(new TreeNode(arg.First().Value, ValueType.Constant));
            }

            if (arg.First().TypeOfValue == ValueType.Variable) // F
            {
                leftMultiply.Add(new TreeNode("1", ValueType.Constant));
                rightMultiply.Add(new TreeNode(arg.First().Value, ValueType.Constant));
            }
            if (arg.Last().TypeOfValue == ValueType.Variable) // G
            {
                rightMultiply.Add(new TreeNode("1", ValueType.Constant));
                leftMultiply.Add(new TreeNode(arg.First().Value, ValueType.Constant));
            }

            numeratorNode.Add(leftMultiply);
            numeratorNode.Add(rightMultiply);

            quotientNode.Add(numeratorNode);

            // Add g^2 Denominator
            TreeNode denominatorNode = new TreeNode("^",ValueType.Operator);
            denominatorNode.Add(new TreeNode(arg.Last().Value, arg.Last().TypeOfValue));
            denominatorNode.Add(new TreeNode("2",ValueType.Constant));
            quotientNode.Add(denominatorNode);

            return null;
        }

        private static TreeNode ProductRule(List<TreeNode> arg)
        {
            TreeNode productNode = new TreeNode("+", ValueType.Operator);

            TreeNode leftMultiply = new TreeNode("*", ValueType.Operator);
            TreeNode rightMultiply = new TreeNode("*", ValueType.Operator);
            
            if (arg.First().TypeOfValue == ValueType.Constant) // F
            {
                leftMultiply.Add(new TreeNode("0", ValueType.Constant));
                rightMultiply.Add(new TreeNode(arg.First().Value, ValueType.Constant));
            }
            if (arg.Last().TypeOfValue == ValueType.Constant) // G
            {
                rightMultiply.Add(new TreeNode("0", ValueType.Constant));
                leftMultiply.Add(new TreeNode(arg.First().Value, ValueType.Constant));
            }

            if (arg.First().TypeOfValue == ValueType.Variable) // F
            {
                leftMultiply.Add(new TreeNode("1", ValueType.Constant));
                rightMultiply.Add(new TreeNode(arg.First().Value, ValueType.Constant));
            }
            if (arg.Last().TypeOfValue == ValueType.Variable) // G
            {
                rightMultiply.Add(new TreeNode("1", ValueType.Constant));
                leftMultiply.Add(new TreeNode(arg.First().Value, ValueType.Constant));
            }
            
            productNode.Add(leftMultiply);
            productNode.Add(rightMultiply);

            return productNode;
        }

        private static TreeNode DifferenceRule(List<TreeNode> arg)
        {
            TreeNode differenceNode = new TreeNode("-", ValueType.Operator);

            foreach (TreeNode treeNodeChild in arg)
            {
                if (treeNodeChild.TypeOfValue == ValueType.Constant)
                {
                    differenceNode.Children.Add(new TreeNode("0", ValueType.Constant));
                }
                if (treeNodeChild.TypeOfValue == ValueType.Variable)
                {
                    differenceNode.Children.Add(new TreeNode("1", ValueType.Constant));
                }
            }

            return differenceNode;
        }

        private static TreeNode SumRule(List<TreeNode> arg)
        {
            TreeNode sumNode = new TreeNode("+", ValueType.Operator);

            foreach (TreeNode treeNodeChild in arg)
            {
                if (treeNodeChild.TypeOfValue == ValueType.Constant)
                {
                    sumNode.Children.Add(new TreeNode("0", ValueType.Constant));
                }
                if (treeNodeChild.TypeOfValue == ValueType.Variable)
                {
                    sumNode.Children.Add(new TreeNode("1", ValueType.Constant));
                }
            }

            return sumNode;
        }
        #endregion
    }
}
