using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Git_Gud_At_Math.Models;

namespace Git_Gud_At_Math.Controls
{
    public static class IntegralCalculator
    {
        public static double CalculateIntegralOf(Function function,double start, double end)
        {
            double result = 0;
            double funcDensity = function.Density;

            Point startOfCalc = new Point(start,
                Calculator.EvaluateFunctionTree(function.FunctionTree.Clone(),
                new Dictionary<string, string>
                {
                    {"x",start.ToString()}
                }));

            Point endOfCalc = new Point(end,
                Calculator.EvaluateFunctionTree(function.FunctionTree.Clone(),
                    new Dictionary<string, string>
                    {
                        {"x",end.ToString()}
                    }));

            int startCalcIndex = (int) Math.Ceiling((start - function.FunctionSolutions.First().X) / funcDensity);
            int endCalcIndex = (int) Math.Floor((end - function.FunctionSolutions.First().X) / funcDensity);
            
            List<Point> pointsToCalcFor = function.FunctionSolutions.GetRange(startCalcIndex, (endCalcIndex - startCalcIndex + 1));

            pointsToCalcFor.Insert(0,startOfCalc);
            pointsToCalcFor.Add(endOfCalc);

            for (int i = 0; i < pointsToCalcFor.Count - 1; i++)
            {
                // Get First Point
                Point a = new Point(pointsToCalcFor[i].X, 0);
                // Get Second Point
                Point b = new Point(pointsToCalcFor[i + 1].X, pointsToCalcFor[i].Y);

                // Calculate Rec
                result +=  Math.Round(CalculateRenctangeArea(a, b), 4);
                result += CalculateTriangle(pointsToCalcFor[i],b, pointsToCalcFor[i + 1]);
            }

            return Math.Round(result,2);
        }

        public static double CalculateRenctangeArea(Point a, Point b)
        {
            double result = 0;

            double width = b.X - a.X;
            double height = b.Y - a.Y;

            result = width * height;
            return result;
        }

        public static double CalculateTriangle(Point a, Point b, Point c)
        {
            double aX = a.X;
            double aY = a.Y;

            double bX = b.X;
            double bY = b.Y;

            double cX = c.X;
            double cY = c.Y;

            double areaTriangle = ((aX * (bY - cY) + bX * (cY - aY) + cX * (aY - bY))) / 2;

            return areaTriangle;
        }
    }
}
