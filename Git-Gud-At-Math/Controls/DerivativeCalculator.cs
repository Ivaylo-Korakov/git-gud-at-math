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
            {"+", SumRule },
            {"-", DifferenceRule },
            {"*", ProductRule },
            {"/", QuotientRule },
            {"^", PowerRule }
        };
        
        public static Function CalculateDerivativeOf(Function function)
        {
            TreeNode derivativeTree = GetDerivativeOfTree(function.FunctionTree.Clone());

            Function derivativeFunction = new Function(derivativeTree);

            return derivativeFunction;
        }

        public static TreeNode GetDerivativeOfTree(TreeNode startNode)
        {
            return null;
        }
        
        #region DerivativeRules
        private static TreeNode SumRule(List<TreeNode> arg)
        {
            TreeNode sumNode = new TreeNode("+", ValueType.Operator);
            return sumNode;
        }

        private static TreeNode DifferenceRule(List<TreeNode> arg)
        {
            TreeNode differenceNode = new TreeNode("-", ValueType.Operator);
            return differenceNode;
        }

        private static TreeNode ProductRule(List<TreeNode> arg)
        {
            TreeNode productNode = new TreeNode("+", ValueType.Operator);
            return productNode;
        }

        private static TreeNode QuotientRule(List<TreeNode> arg)
        {
            TreeNode quotientNode = new TreeNode("/", ValueType.Operator);
            return null;
        }

        private static TreeNode PowerRule(List<TreeNode> arg)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
