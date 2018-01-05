using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Git_Gud_At_Math.Controls;
using Git_Gud_At_Math.Utilities;

namespace Git_Gud_At_Math.Drawing
{
    public class FunctionEditor
    {
        public MainWindow Window { get; private set; }
        public Painter Painter { get; private set; }
        public List<Point> FunctionPoints { get; private set; }
        public bool IsActive = false;

        public FunctionEditor(MainWindow mainWindow)
        {
            this.Window = mainWindow;
            this.Painter = this.Window.Painter;
            FunctionPoints = new List<Point>();
        }

        public void GenerateFunction()
        {
            // Sort all the points
            this.FunctionPoints = this.FunctionPoints.OrderBy(a => a.X).ToList();

            // Get the coefficient matrix
            var coefMtrx = GenerateCoefficientMatrix();

            // Calculate the determinant of the coefficient matrix
            double determinant = FunctionCalculator.Determinant(coefMtrx);

            Debug.Print2DMatrix(coefMtrx, true);
            Console.WriteLine("===");
            Console.WriteLine(determinant);
        }

        public double[,] GenerateCoefficientMatrix()
        {
            int matrixSize = this.FunctionPoints.Count;
            double[,] matrix = new double[matrixSize, matrixSize];

            for (int i = 0; i < this.FunctionPoints.Count; i++)
            {
                // Get the point
                var point = this.FunctionPoints[i];
                var x = point.X;

                int power = matrixSize - 1;
                for (int j = 0; j < this.FunctionPoints.Count; j++)
                {
                    matrix[i, j] = Math.Round(Math.Pow(x, power),4);
                    power--;
                }
            }

            return matrix;
        }

        public void Reset()
        {
            this.FunctionPoints.Clear();
            this.Painter.ReDrawCanvas();
        }

        public void Enable()
        {
            // Check if it was active until now
            if (this.IsActive) return;

            this.Window.CreativeModeBtn.Content = "Calculate New Function";
            this.Window.CreativeModeBtn.Background = Brushes.LimeGreen;
            this.Window.CancelEditMode.IsEnabled = true;
            this.IsActive = true;

            // Redraw Canvas
            this.Painter.ReDrawCanvas();
        }

        public void Disable()
        {
            // Check if it was inactive until now
            if (this.IsActive == false) return;

            SolidColorBrush mySolidColorBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#3399ff"));
            this.Window.CreativeModeBtn.Content = "Enter creative mode";
            this.Window.CreativeModeBtn.Background = mySolidColorBrush;
            this.Window.CancelEditMode.IsEnabled = false;
            this.IsActive = false;

            this.Reset();
        }

        public void AddPoint(Point point)
        {
            if (this.IsActive == false) return;

            // Translate to normal ordinates
            var translatedPoint = Painter.TranslatePositionOposite(point);
            // Round point to 4
            translatedPoint.X = Math.Round(translatedPoint.X, 4);
            translatedPoint.Y = Math.Round(translatedPoint.Y, 4);

            // Add point to the rest
            this.FunctionPoints.Add(translatedPoint);

            // Draw the point on the canvas
            this.Painter.DrawPoint(translatedPoint);

            Debug.OutPutAttention("New Point:  " + translatedPoint);
        }
    }
}
