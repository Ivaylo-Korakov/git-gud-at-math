using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Git_Gud_At_Math.Models;
using Git_Gud_At_Math.Utilities;

namespace Git_Gud_At_Math.Drawing
{
    public class Painter
    {
        public Canvas Canvas { get; private set; }
        public MainWindow Window { get; private set; }
        public double CanvasScale = 40;
        public int CanvasMinValue => -500 / (int) this.CanvasScale;
        public int CanvasMaxValue => 500 / (int) this.CanvasScale;
        public int NormalThickness = 2;
        public int FunctionThickness = 5;
        public Brush DefaultBrush = Brushes.Black;
        public Brush FunctionBrush = Brushes.Crimson;
        public Brush AttentionBrush = Brushes.DodgerBlue;
        public List<Function> SolutionsOfFunctions => Window.Controller.Functions;
        public List<Point> FunctionEditPoints => Window.FunctionEditor.FunctionPoints;
        public bool IsEditModeActive => this.Window.FunctionEditor.IsActive;

        public Painter(MainWindow mainWindow)
        {
            this.Window = mainWindow;
            this.Canvas = this.Window.Canvas;
            this.ResetCanvas();
        }

        public void DrawGrid()
        {
            // Draw grid lines
            for (int i = this.CanvasMinValue; i < this.CanvasMaxValue; i++)
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

        public void DrawPoint(Point point)
        {
            Ellipse myPoint = new Ellipse();
            myPoint.Fill = Brushes.Crimson;
            myPoint.Width = 10;
            myPoint.Height = 10;

            // Translate point into canvas cords
            point = this.TranslatePosition(point);

            double left = point.X - (myPoint.Width / 2);
            double top = point.Y - (myPoint.Height / 2);

            myPoint.Margin = new Thickness(left, top, 0, 0);

            this.Canvas.Children.Add(myPoint);
        }

        public void DrawPoints(List<Point> allPoints)
        {
            foreach (var point in allPoints)
            {
                this.DrawPoint(point);
            }
        }

        public void DrawPoints()
        {
            foreach (var point in this.FunctionEditPoints)
            {
                this.DrawPoint(point);
            }
        }

        public void ReDrawCanvas()
        {
            this.ResetCanvas();
            this.DrawPoints();
            this.DrawEditModeMessages();
            
            if (this.IsEditModeActive == false)
            {
                this.DrawFunctions();
            }
        }

        public void DrawEditModeMessages()
        {
            if (this.IsEditModeActive)
            {
                TextBlock editModeMessage = new TextBlock
                {
                    Text = "Function Edit Mode",
                    FontSize = 24,
                    Foreground = Brushes.Lime
                };
                Canvas.SetLeft(editModeMessage, 10);
                Canvas.SetTop(editModeMessage, 0);

                TextBlock editModeInfoMessage = new TextBlock
                {
                    Text = "Other functions are hidden!",
                    FontSize = 14,
                    Foreground = Brushes.White
                };
                Canvas.SetLeft(editModeInfoMessage, 10);
                Canvas.SetTop(editModeInfoMessage, 25);

                this.Canvas.Children.Add(editModeMessage);
                this.Canvas.Children.Add(editModeInfoMessage);
            }
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

        public void ChangeZoomLevel(double newZoomLvl)
        {
            this.CanvasScale = newZoomLvl;
            this.ReDrawCanvas();
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

        public Point TranslatePositionOposite(Point point)
        {
            // Get canvas size
            double width = this.Canvas.ActualWidth;
            double height = this.Canvas.ActualHeight;

            double centerX = width / 2;
            double centerY = height / 2;

            double finalX = point.X - centerX;
            double finalY = point.Y - centerY;

            // Apply scale / Downscale
            finalX = finalX / this.CanvasScale;
            finalY = finalY / this.CanvasScale;

            // Invert y
            finalY *= -1;

            return new Point(finalX,finalY);
        }
    }
}
