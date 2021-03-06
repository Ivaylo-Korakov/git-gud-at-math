﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Git_Gud_At_Math.Models;
using ValueType = Git_Gud_At_Math.Models.ValueType;
using Git_Gud_At_Math.Utilities;

namespace Git_Gud_At_Math.Controls
{
    public static class DerivativeCalculator
    {
        // How close we are able to calculate the Newton Quotient for given function
        public const double NewtonQuotientH = 0.01;

        public static Dictionary<string, Func<List<TreeNode>, TreeNode>> DerivativeRules = new Dictionary<string, Func<List<TreeNode>, TreeNode>>()
        {
            {"+", SumRule },
            {"-", DifferenceRule },
            {"*", ProductRule },
            {"/", QuotientRule },
            {"^", PowerRule },
            {"s", SinRule },
            {"c", CosineRule },
            {"l", LogRule },
            {"e", ExponentialRule }
        };

        public static Function CalculateNewtonQuotient(Function function, Dictionary<string, string> variables, string variableToCalculateFor, double startPoint, double endPoint, double density)
        {
            density = Math.Abs(density);

            if (startPoint > endPoint)
            {
                double temp = startPoint;
                startPoint = endPoint;
                endPoint = temp;
            }

            List<Point> solutions = new List<Point>();

            for (double position = startPoint; position < endPoint; position += density)
            {
                variables[variableToCalculateFor] = (position + NewtonQuotientH).ToString();
                double a = Calculator.EvaluateFunctionTree(function.FunctionTree.Clone(), variables);

                variables[variableToCalculateFor] = position.ToString();
                double b = Calculator.EvaluateFunctionTree(function.FunctionTree.Clone(), variables);

                double result = (a - b) / NewtonQuotientH;

                // Debug purposes
                Debug.OutPutWarning("a: " + a);
                Debug.OutPutWarning("b: " + b);
                Debug.OutPutWarning("result: " + result);

                solutions.Add(new Point(position, result));
            }

            Function returnFunction = new Function(solutions);
            return returnFunction;
        }

        public static Function CalculateDerivativeOf(Function function)
        {
            TreeNode derivativeTree = GetDerivativeOfTree(function.FunctionTree.Clone());

            Function derivativeFunction = new Function(derivativeTree);

            return derivativeFunction;
        }

        public static TreeNode GetDerivativeOfTree(TreeNode startNode)
        {
            if (startNode.TypeOfValue == ValueType.Unknown)
            {
                return GetDerivativeOfTree(startNode.Children.First());
            }

            if (startNode.TypeOfValue == ValueType.Operator)
            {
                if (DerivativeRules.ContainsKey(startNode.Value))
                {
                    return DerivativeRules[startNode.Value](startNode.Children);
                }
            }

            if (startNode.TypeOfValue == ValueType.Constant)
            {
                return new TreeNode("0", ValueType.Constant);
            }

            return new TreeNode("1", ValueType.Constant);
        }

        public static TreeNode GetDerivativeOfN(TreeNode startNode, int n)
        {
            for (int i = 0; i < n; i++)
            {
                startNode = GetDerivativeOfTree(startNode.Clone());
                startNode = TreeSimplifier.Simplify(startNode.Clone());
            }
            
            return startNode;
        }

        #region DerivativeRules
        private static TreeNode SumRule(List<TreeNode> args)
        {
            TreeNode sumNode = new TreeNode("+", ValueType.Operator);

            foreach (var arg in args)
            {
                sumNode.Add(GetDerivativeOfTree(arg.Clone()));
            }

            return sumNode;
        }

        private static TreeNode DifferenceRule(List<TreeNode> args)
        {
            TreeNode differenceNode = new TreeNode("-", ValueType.Operator);

            foreach (var arg in args)
            {
                differenceNode.Add(GetDerivativeOfTree(arg.Clone()));
            }

            return differenceNode;
        }

        private static TreeNode ProductRule(List<TreeNode> args)
        {
            TreeNode productNode = new TreeNode("+", ValueType.Operator);

            TreeNode a = args.First();
            TreeNode b = args.Last();

            TreeNode leftMultiply = new TreeNode("*", ValueType.Operator);
            TreeNode rightMultiply = new TreeNode("*", ValueType.Operator);

            leftMultiply.Add(GetDerivativeOfTree(a.Clone()));
            leftMultiply.Add(b.Clone());

            rightMultiply.Add(a.Clone());
            rightMultiply.Add(GetDerivativeOfTree(b.Clone()));

            productNode.Add(leftMultiply);
            productNode.Add(rightMultiply);

            return productNode;
        }

        private static TreeNode QuotientRule(List<TreeNode> args)
        {
            TreeNode quotientNode = new TreeNode("/", ValueType.Operator);

            TreeNode a = args.First();
            TreeNode b = args.Last();

            TreeNode nominator = new TreeNode("-", ValueType.Operator);
            TreeNode denominator = new TreeNode("^", ValueType.Operator);

            // Nominator
            TreeNode leftMultiply = new TreeNode("*", ValueType.Operator);
            TreeNode rightMultiply = new TreeNode("*", ValueType.Operator);

            leftMultiply.Add(GetDerivativeOfTree(a.Clone()));
            leftMultiply.Add(b.Clone());

            rightMultiply.Add(a.Clone());
            rightMultiply.Add(GetDerivativeOfTree(b.Clone()));

            nominator.Add(leftMultiply);
            nominator.Add(rightMultiply);

            // Denominator
            denominator.Add(b);
            denominator.Add(new TreeNode("2", ValueType.Constant));

            quotientNode.Add(nominator);
            quotientNode.Add(denominator);

            return quotientNode;
        }

        private static TreeNode PowerRule(List<TreeNode> args)
        {
            TreeNode x = args.First();
            TreeNode n = args.Last();

            TreeNode powerNode = new TreeNode("*", ValueType.Operator);
            powerNode.Add(n.Clone());

            TreeNode powerNodeTwo = new TreeNode("*", ValueType.Operator);
            powerNodeTwo.Add(GetDerivativeOfTree(x.Clone()));

            TreeNode rightMultiply = new TreeNode("^", ValueType.Operator);
            rightMultiply.Add(x.Clone());

            TreeNode minus = new TreeNode("-", ValueType.Operator);
            minus.Add(n.Clone());
            minus.Add(new TreeNode("1", ValueType.Constant));

            rightMultiply.Add(minus);
            powerNodeTwo.Add(rightMultiply);

            powerNode.Add(powerNodeTwo);

            return powerNode;
        }

        private static TreeNode SinRule(List<TreeNode> args)
        {
            TreeNode x = args.First();

            TreeNode multiply = new TreeNode("*", ValueType.Operator);

            TreeNode cos = new TreeNode("c", ValueType.Operator);
            cos.Add(x.Clone());

            multiply.Add(cos);
            multiply.Add(GetDerivativeOfTree(x.Clone()));

            return multiply;
        }

        private static TreeNode CosineRule(List<TreeNode> args)
        {
            TreeNode x = args.First();

            TreeNode multiply = new TreeNode("*", ValueType.Operator);

            TreeNode minus = new TreeNode("-", ValueType.Operator);
            minus.Add(new TreeNode("0", ValueType.Constant));

            TreeNode cos = new TreeNode("s", ValueType.Operator);
            cos.Add(x.Clone());

            minus.Add(cos);
            multiply.Add(minus);
            multiply.Add(GetDerivativeOfTree(x.Clone()));

            return multiply;
        }

        private static TreeNode LogRule(List<TreeNode> args)
        {
            TreeNode x = args.First();

            TreeNode multiply = new TreeNode("*", ValueType.Operator);

            TreeNode devide = new TreeNode("/", ValueType.Operator);
            devide.Add(new TreeNode("1", ValueType.Constant));

            devide.Add(x.Clone());

            multiply.Add(devide);
            multiply.Add(GetDerivativeOfTree(x.Clone()));

            return multiply;
        }

        private static TreeNode ExponentialRule(List<TreeNode> args)
        {
            TreeNode x = args.First();

            TreeNode multiply = new TreeNode("*", ValueType.Operator);

            TreeNode power = new TreeNode("e", ValueType.Operator);
            power.Add(x.Clone());

            multiply.Add(power);
            multiply.Add(GetDerivativeOfTree(x.Clone()));

            return multiply;
        }
        #endregion
    }
}
