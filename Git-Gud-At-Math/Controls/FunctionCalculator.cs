using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Git_Gud_At_Math.Models;
using ValueType = Git_Gud_At_Math.Models.ValueType;

namespace Git_Gud_At_Math.Controls
{
    public static class FunctionCalculator
    {

        #region Public
        /// <summary>
        /// This method determines the value of determinant using recursion
        /// </summary>
        /// <param name="input">Input matrix to calculate for</param>
        /// <returns>The determinant of that matrix</returns>
        public static double Determinant(double[,] input)
        {
            int order = int.Parse(System.Math.Sqrt(input.Length).ToString());
            if (order > 2)
            {
                double value = 0;
                for (int j = 0; j < order; j++)
                {
                    double[,] Temp = CreateSmallerMatrix(input, 0, j);
                    value = value + input[0, j] * (SignOfElement(0, j) * Determinant(Temp));
                }
                return value;
            }
            else if (order == 2)
            {
                return ((input[0, 0] * input[1, 1]) - (input[1, 0] * input[0, 1]));
            }
            else
            {
                return (input[0, 0]);
            }
        }

        public static TreeNode GenerateFumcTreeFromCoefficients(List<double> coefficients)
        {
            TreeNode sum = new TreeNode("+",ValueType.Operator);

            int power = coefficients.Count - 1;
            foreach (var coefficient in coefficients)
            {
                TreeNode multiplyNode = new TreeNode("*", ValueType.Operator);
                multiplyNode.Add(new TreeNode(coefficient.ToString(),ValueType.Constant));

                TreeNode powerNode = new TreeNode("^",ValueType.Operator);
                powerNode.Add(new TreeNode("x",ValueType.Variable));
                powerNode.Add(new TreeNode(power.ToString(),ValueType.Constant));
                
                multiplyNode.Add(powerNode);
                sum.Add(multiplyNode);

                power--;
            }

            return sum;
        }

        public static double[,] ReplaceCol(double[,] matrix, List<double> points, int colNumber)
        {
            try
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    matrix[i, colNumber] = points[i];
                }
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }

            return matrix;
        }

        #endregion

        #region Private
        /// <summary>
        /// This method determines the sub matrix corresponding to a given element
        /// </summary>
        private static double[,] CreateSmallerMatrix(double[,] input, int i, int j)
        {
            int order = int.Parse(System.Math.Sqrt(input.Length).ToString());
            double[,] output = new double[order - 1, order - 1];
            int x = 0, y = 0;
            for (int m = 0; m < order; m++, x++)
            {
                if (m != i)
                {
                    y = 0;
                    for (int n = 0; n < order; n++)
                    {
                        if (n != j)
                        {
                            output[x, y] = input[m, n];
                            y++;
                        }
                    }
                }
                else
                {
                    x--;
                }
            }
            return output;
        }

        /// <summary>
        /// this method determines the sign of the elements
        /// </summary>
        private static int SignOfElement(int i, int j)
        {
            if ((i + j) % 2 == 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        #endregion


    }
}
