using System.Collections.Generic;
using System.Linq;
using Git_Gud_At_Math.Exceptions;
using Git_Gud_At_Math.Models;
using ValueType = Git_Gud_At_Math.Models.ValueType;

namespace Git_Gud_At_Math.Controls
{
    public static class TreeSimplifier
    {
        public static List<string> SupportedFunctions = new List<string>()
        {
            "+", "-", "*", "/", "^"
        };

        public static TreeNode Simplify(TreeNode startNode)
        {
            var tempWorkNode = SimplifySplit(startNode.Clone()); 
            tempWorkNode = SimplifyCalculations(tempWorkNode.Clone());

            return tempWorkNode;
        }

        public static TreeNode SimplifySplit(TreeNode startNode)
        {
            if (startNode.TypeOfValue == ValueType.Unknown)
            {
                return SimplifySplit(startNode.Children.First().Clone());
            }

            if (startNode.TypeOfValue == ValueType.Constant ||
                startNode.TypeOfValue == ValueType.Variable)
            {
                return startNode;
            }

            // Check if operator is supported
            if (SupportedFunctions.Contains(startNode.Value))
            {
                // Check if children count > 2
                if (startNode.Children.Count > 2)
                {
                    // Create new Node
                    TreeNode newLinkNode = new TreeNode(startNode.Value, ValueType.Operator);

                    int count = 0;
                    foreach (var startNodeChild in startNode.Children)
                    {
                        if (count == 0)
                        {
                            count++;
                            continue;
                        }
                        newLinkNode.Add(SimplifySplit(startNodeChild.Clone()));
                    }

                    var firstChild = startNode.Children.First().Clone();
                    startNode.Children.Clear();
                    startNode.Add(SimplifySplit(firstChild.Clone()));
                    startNode.Add(SimplifySplit(newLinkNode.Clone()));

                    return startNode;
                }

                // Else
                return startNode;
            }

            // Check if children is one
            if (startNode.Children.Count == 1)
            {
                return startNode;
            }

            throw new UnparseableString("Operator \"" + startNode.Value + "\" does not support more than one argument!");
        }

        public static TreeNode SimplifyCalculations(TreeNode startNode)
        {
            if (startNode.TypeOfValue == ValueType.Unknown)
            {
                return SimplifyCalculations(startNode);
            }

            bool isOnlyConstants = true;
            if (startNode.TypeOfValue == ValueType.Operator)
            {
                // Check Children
                for (int i = 0; i < startNode.Children.Count; i++)
                {
                    if (startNode.Children[i].TypeOfValue == ValueType.Variable)
                    {
                        isOnlyConstants = false;
                    }

                    if (startNode.Children[i].TypeOfValue == ValueType.Operator)
                    {
                        startNode.Children[i] = SimplifyCalculations(startNode.Children[i].Clone());
                        if (startNode.Children[i].TypeOfValue == ValueType.Operator || startNode.Children[i].TypeOfValue == ValueType.Variable)
                        {
                            isOnlyConstants = false;
                        }
                    }
                }

                // Special case power
                if (startNode.Value == "^")
                {
                    if (startNode.Children.Last().Value == "1")
                    {
                        return startNode.Children.First().Clone();
                    }

                    if (startNode.Children.Last().Value == "0")
                    {
                        return new TreeNode("1", ValueType.Constant);
                    }
                }

                // Special case multiply
                if (startNode.Value == "*")
                {
                    // Check children
                    foreach (var startNodeChild in startNode.Children)
                    {
                        if (startNodeChild.Value == "0")
                        {
                            return new TreeNode("0", ValueType.Constant);
                        }
                    }

                    if (startNode.Children.First().Value == "1")
                    {
                        return startNode.Children.Last().Clone();
                    }

                    if (startNode.Children.Last().Value == "1")
                    {
                        return startNode.Children.First().Clone();
                    }
                }

                if (isOnlyConstants)
                {
                    return new TreeNode(Calculator.CalculateSimpleNodeTree(startNode).ToString(), ValueType.Constant);
                }
            }

            return startNode;
        }
    }
}