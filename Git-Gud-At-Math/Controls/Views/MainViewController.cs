using System;
using System.Collections.Generic;
using System.Windows;
using Git_Gud_At_Math.Exceptions;
using Git_Gud_At_Math.Models;
using Git_Gud_At_Math.Utilities;
using ValueType = Git_Gud_At_Math.Models.ValueType;

namespace Git_Gud_At_Math.Controls.Views
{
    public class MainViewController
    {
        public MainWindow Window { get; private set; }
        public Function CurrentSelectedFunction { get; set; }
        public List<Function> Functions { get; set; }
        public static Dictionary<string, Window> OpenedFunctionTreeWindows = new Dictionary<string, Window>();

        public delegate void NewCalcFunctionDel();

        public event NewCalcFunctionDel FunctionUpdated;

        public MainViewController(MainWindow mainWindow)
        {
            this.Window = mainWindow;
            this.Functions = new List<Function>();
            this.FunctionUpdated += this.Window.Painter.ReDrawCanvas;

            // Test data
            // /(*(-(x,3),+(c(n(-73)),r(1.6))),e(!(5)))
            // /(-(+(x,1),*(x,3)),^(x,2))
            // *(^(x,2),!(5))
            // ^(x,2)
            // +(*(c(x),2),*(s(*(2,x)),c(*(60,x))))
            // /(c(x),s(x))
            // /(c(^(x,*(2,2))),s(x))
            // /(c(^(1,*(2,2))),s(x))
            // ^(x,2,^(x,2,2))
            // ^(x,2,^(x,^(2,2,2),2))
        }

        public void AddFunction(Function functionToAdd)
        {
            this.Functions.Add(functionToAdd);
            functionToAdd.Calculate(-25, 25, 0.1);
            this.Window.FunctionView.Items.Add(functionToAdd);
            FunctionUpdated();
        }

        public void RemoveFunction(Function functionToRemove)
        {
            this.Functions.Remove(functionToRemove);
            FunctionUpdated();
        }

        public void ShowFunctionTree()
        {
            if (this.CurrentSelectedFunction != null)
            {
                // Check if it already exists
                if (OpenedFunctionTreeWindows.ContainsKey(this.CurrentSelectedFunction.FunctionAsString))
                {
                    OpenedFunctionTreeWindows[this.CurrentSelectedFunction.FunctionAsString].Focus();
                }
                else
                {
                    TreeGraphWindow newGraphWindow = new TreeGraphWindow(this.CurrentSelectedFunction);
                    OpenedFunctionTreeWindows.Add(this.CurrentSelectedFunction.FunctionAsString, newGraphWindow);
                    newGraphWindow.Show();
                }
            }
        }
    }
}
