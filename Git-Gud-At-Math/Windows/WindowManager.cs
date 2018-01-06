using System.Collections.Generic;
using System.Windows;
using Git_Gud_At_Math.Models;

namespace Git_Gud_At_Math.Windows
{
    public static class WindowManager
    {
        public static Dictionary<int, Window> OpenedWindows = new Dictionary<int, Window>();
        public static Dictionary<string, Window> OpenedFunctionTreeWindows = new Dictionary<string, Window>();

        public static void OpenWindow(Window newWindowToOpen)
        {
            if (OpenedWindows.ContainsKey(newWindowToOpen.GetHashCode()))
            {
                // Focus
                OpenedWindows[newWindowToOpen.GetHashCode()].Focus();
            }
            else
            {
                // Open
                OpenedWindows.Add(newWindowToOpen.GetHashCode(), newWindowToOpen);
                newWindowToOpen.Show();
            }
        }

        public static void OpenFunctionWindow(Function functionForWindow)
        {
            // Check if it already exists
            if (OpenedFunctionTreeWindows.ContainsKey(functionForWindow.FunctionAsString))
            {
                if (OpenedFunctionTreeWindows[functionForWindow.FunctionAsString].IsLoaded)
                {
                    OpenedFunctionTreeWindows[functionForWindow.FunctionAsString].Focus();
                }
                else
                {
                    TreeGraphWindow newGraphWindow = new TreeGraphWindow(functionForWindow);
                    OpenedFunctionTreeWindows[functionForWindow.FunctionAsString] = newGraphWindow;
                    newGraphWindow.Show();
                }
            }
            else
            {
                TreeGraphWindow newGraphWindow = new TreeGraphWindow(functionForWindow);
                OpenedFunctionTreeWindows.Add(functionForWindow.FunctionAsString, newGraphWindow);
                newGraphWindow.Show();
            }
        }

        public static void CloseWindow(Window windowToClose)
        {
            if (OpenedWindows.ContainsKey(windowToClose.GetHashCode()))
            {
                OpenedWindows.Remove(windowToClose.GetHashCode());
            }
        }
    }
}
