using System;
using System.Windows;
using Git_Gud_At_Math.Utilities;
using LiveCharts;
using LiveCharts.Defaults;

namespace Git_Gud_At_Math.Controls.MachineLearning
{
    public static class MachineLearningCalculator
    {
        public const double LeariningRate = 0.01;

        public static Tuple<double, double> GradientDescent(double[,] data, double thetaOne, double thetaTwo, ChartValues<ObservablePoint> hypothesis, double interations)
        {
            Debug.OutPut("--- INITIAL ---" + thetaOne + " " + thetaTwo);
            double m = data.GetLength(0);

            int count = 0;
            while (count < interations)
            {
                double sumOne = 0;
                double sumTwo = 0;
                for (int i = 0; i < m; i++)
                {
                    Point currentPoint = new Point(data[i, 0], data[i, 1]);
                    double modelValue = SolveRegressionModel(currentPoint.X, thetaOne, thetaTwo);
                    sumOne += modelValue - currentPoint.Y;
                }
                double tempThetaOne = thetaOne - LeariningRate * (1 / m) * sumOne;

                for (int i = 0; i < m; i++)
                {
                    Point currentPoint = new Point(data[i, 0], data[i, 1]);
                    double modelValue = SolveRegressionModel(currentPoint.X, thetaOne, thetaTwo);
                    sumTwo += (modelValue - currentPoint.Y) * currentPoint.X;
                }
                double tempThetaTwo = thetaTwo - LeariningRate * (1 / m) * sumTwo;

                thetaOne = tempThetaOne;
                thetaTwo = tempThetaTwo;
                Debug.OutPut("Iter: " + count + " T0: " + thetaOne + " T1: " + thetaTwo);
                count++;

                if (count % 10 == 0)
                {
                    // Add for first
                    var x = 0;
                    var y = thetaOne + thetaTwo * x;
                    var a = new ObservablePoint(x, y);

                    x = 20;
                    y = thetaOne + thetaTwo * x;
                    var b = new ObservablePoint(x, y);

                    hypothesis.Clear();
                    hypothesis.Add(a);
                    hypothesis.Add(b);
                }
            }

            return new Tuple<double, double>(thetaOne, thetaTwo);
        }

        public static double SolveRegressionModel(double x, double thetaOne, double thetaTwo)
        {
            return thetaOne + thetaTwo * x;
        }
    }
}
