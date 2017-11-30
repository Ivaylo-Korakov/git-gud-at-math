using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using Git_Gud_At_Math.Models;
using Git_Gud_At_Math.Utilities;

namespace Git_Gud_At_Math.Drawing
{
    public class Painter
    {
        public Canvas Canvas { get; private set; }
        public MainWindow Window { get; private set; }
        public double CanvasScale = 40;
        public int NormalThickness = 2;
        public int FunctionThickness = 5;
        public Brush DefaultBrush = Brushes.Black;
        public Brush FunctionBrush = Brushes.Crimson;
        public Brush AttentionBrush = Brushes.DodgerBlue;
        public List<Function> SolutionsOfFunctions => Window.Controller.Functions;

        public Painter(MainWindow mainWindow)
        {
            this.Window = mainWindow;
            this.Canvas = this.Window.Canvas;
            this.ResetCanvas();
        }

        public void DrawGrid()
        {
            // Draw grid lines
            for (int i = -400 / (int)this.CanvasScale; i < 400 / (int)this.CanvasScale; i++)
            {
                // x line
                this.DrawLine(new Point(this.Canvas.ActualWidth * -1, i), new Point(this.Canvas.ActualWidth, i), DefaultBrush, NormalThickness);
                // y line
                this.DrawLine(new Point(i, this.Canvas.ActualHeight * -1), new Point(i, this.Canvas.ActualHeight), DefaultBrush, NormalThickness);
            }

            // Draw two main lines
            // x line
            this.DrawLine(new Point(this.Canvas.ActualWidth * -1, 0), new Point(this.Canvas.ActualWidth, 0), AttentionBrush, NormalThickness);
            // y line
            this.DrawLine(new Point(0, this.Canvas.ActualHeight * -1), new Point(0, this.Canvas.ActualHeight), AttentionBrush, NormalThickness);
        }

        public void ReDrawCanvas()
        {
            this.ResetCanvas();
            this.DrawFunctions();
        }

        public void DrawFunctions()
        {
            foreach (Function function in this.SolutionsOfFunctions)
            {
                this.DrawFunction(function);
            }
        }

        public void ResetCanvas()
        {
            this.Canvas.Children.Clear();
            this.DrawGrid();
        }

        public void DrawLine(Point a, Point b, Brush brushToUse, int thickness)
        {
            a = TranslatePosition(a);
            b = TranslatePosition(b);

            var newLine = new Line
            {
                Stroke = brushToUse,
                X1 = a.X,
                X2 = b.X,
                Y1 = a.Y,
                Y2 = b.Y,
                StrokeThickness = thickness
            };

            this.Canvas.Children.Add(newLine);
        }

        public void DrawFunction(Function func)
        {
            if (func.IsVisible == false) return;
            
            BrushConverter bc = new BrushConverter();
            Brush tempBrush = (Brush)bc.ConvertFrom(Converters.HexConverter(func.FunctionColor));

            for (int index = 0; index < func.FunctionSolutions.Count - 1; index++)
            {
                this.DrawLine(func.FunctionSolutions[index], func.FunctionSolutions[index + 1], tempBrush, this.FunctionThickness);
            }
        }

        public Point TranslatePosition(Point point)
        {
            Debug.OutPut("Point: " + point);

            // Get canvas size
            double width = this.Canvas.ActualWidth;
            double height = this.Canvas.ActualHeight;

            double centerX = width / 2;
            double centerY = height / 2;

            // Flip the y
            point.Y = point.Y * -1;

            // Apply scale
            point.X = point.X * this.CanvasScale;
            point.Y = point.Y * this.CanvasScale;

            var newPoint = new Point(centerX + point.X, centerY + point.Y);

            Debug.OutPut(" -> " + newPoint + " | " + width + " : " + height);
            return newPoint;
        }
    }
}
