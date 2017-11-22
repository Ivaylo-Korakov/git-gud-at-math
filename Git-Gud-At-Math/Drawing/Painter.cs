using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using Git_Gud_At_Math.Utilities;

namespace Git_Gud_At_Math.Drawing
{
    public class Painter
    {
        public Canvas Canvas { get; private set; }
        public double CanvasScale = 50;
        public Brush DefaultBrush = Brushes.LightSteelBlue;
        public Brush FunctionBrush = Brushes.Crimson;
        public Brush AttentionBrush = Brushes.DodgerBlue;

        public Painter(Canvas windowCanvas)
        {
            this.Canvas = windowCanvas;
            ResetCanvas();
        }

        public void DrawGrid()
        {
            // Draw two main lines
            // x line
            DrawLine(new Point(this.Canvas.ActualWidth * -1, 0),new Point(this.Canvas.ActualWidth,0), AttentionBrush);
            // x line
            DrawLine(new Point(0, this.Canvas.ActualHeight * -1), new Point(0, this.Canvas.ActualHeight), AttentionBrush);
        }

        public void NewFunctionToDraw(List<Point> points)
        {
            ResetCanvas();
            DrawLines(points);
        }

        public void DrawLine(Point a, Point b,Brush brushToUse)
        {
            a = TranslatePosition(a);
            b = TranslatePosition(b);

            double thickness = 2;

            // Check if function
            if (Equals(brushToUse, this.FunctionBrush))
            {
                thickness = 4;
            }

            var newLine = new Line
            {
                Stroke = brushToUse,
                X1 = a.X,
                X2 = b.X,
                Y1 = a.Y,
                Y2 = b.Y,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                StrokeThickness = thickness
            };


            this.Canvas.Children.Add(newLine);
        }

        public void DrawLines(List<Point> points)
        {
            for (int index = 0; index < points.Count - 1; index++)
            {
                DrawLine(points[index],points[index + 1], FunctionBrush);
            }
        }

        public Point TranslatePosition(Point point)
        {
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

            Debug.OutPut(point + " -> " + newPoint + " | " + width + " : " + height);
            return newPoint;
        }

        public void ResetCanvas()
        {
            this.Canvas.Children.Clear();
            DrawGrid();
        }
    }
}
