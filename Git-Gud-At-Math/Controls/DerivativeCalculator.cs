//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Git_Gud_At_Math.Models;
//using ValueType = Git_Gud_At_Math.Models.ValueType;

//namespace Git_Gud_At_Math.Controls
//{
//    public static class DerivativeCalculator
//    {
//        public static Dictionary<string, Func<List<TreeNode>, TreeNode>> Operations = new Dictionary<string, Func<List<TreeNode>, TreeNode>>()
//        {
//            {"+",SumRule },
//            {"-",DifferenceRule },
//            {"*",ProductRule },
//            {"/",QuotientRule },
//            {"^",PowerRule }
//        };

//        private static TreeNode PowerRule(List<TreeNode> arg)
//        {
//            throw new NotImplementedException();
//        }


//        public static Function CalculateDerivativeOf(Function function)
//        {
//            TreeNode derivativeTree = GetDerivativeOfTree(function.FunctionTree.Clone());

//            Function derivativeFunction = new Function(derivativeTree);

//            return derivativeFunction;
//        }

//        public static TreeNode GetDerivativeOfTree(TreeNode startNode)
//        {
//            if (startNode.TypeOfValue == ValueType.Operator)
//            {
//                bool isChildAnOperator = false;
//                // Check if there is operator in children
//                foreach (var startNodeChild in startNode.Children)
//                {
//                    if (startNodeChild.TypeOfValue == ValueType.Operator)
//                    {
//                        isChildAnOperator = true;
//                        GetDerivativeOfTree(startNodeChild);
//                    }
//                }

//                // Else - Simple Tree
//                if (isChildAnOperator == false)
//                {
//                    var newDerivedTree = GetDerivativeOfSimpleTree(startNode.Clone());
//                    newDerivedTree.Parent = startNode.Parent;
//                    startNode.Parent.Children.Add(newDerivedTree);
//                    startNode.Parent.Children.Remove(startNode);
//                }
//            }
//        }

//        public static TreeNode GetDerivativeOfSimpleTree(TreeNode startNode)
//        {
            
//        }

//        #region DerivativeRules
//        private static TreeNode QuotientRule(List<TreeNode> arg)
//        {

//        }

//        private static TreeNode ProductRule(List<TreeNode> arg)
//        {

//        }

//        private static TreeNode DifferenceRule(List<TreeNode> arg)
//        {
//            TreeNode differenceNode = new TreeNode("-", ValueType.Operator);

//            foreach (TreeNode treeNodeChild in arg)
//            {
//                if (treeNodeChild.TypeOfValue == ValueType.Constant)
//                {
//                    differenceNode.Children.Add(new TreeNode("0", ValueType.Constant));
//                }
//                if (treeNodeChild.TypeOfValue == ValueType.Variable)
//                {
//                    differenceNode.Children.Add(new TreeNode("1", ValueType.Constant));
//                }
//            }

//            return differenceNode;
//        }

//        private static TreeNode SumRule(List<TreeNode> arg)
//        {
//            TreeNode sumNode = new TreeNode("+",ValueType.Operator);

//            foreach (TreeNode treeNodeChild in arg)
//            {
//                if (treeNodeChild.TypeOfValue == ValueType.Variable)
//                {
//                    sumNode.Children.Add(new TreeNode("1",ValueType.Constant));
//                }
//            }

//            return sumNode;
//        }
//        #endregion
//    }
//}
