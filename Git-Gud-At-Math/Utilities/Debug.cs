using System;
using System.Linq;
using Git_Gud_At_Math.Models;

namespace Git_Gud_At_Math.Utilities
{
    /// <summary>
    /// All the output information for debugging goes trough this class.
    /// That way we can easily switch the debug output ON or OFF.
    /// All the methods are pretty self explanatory.
    /// </summary>
    public static class Debug
    {
        public static bool IsApplicationInDebugMode = false;

        public static void OutPut<T>(T a)
        {
            if (IsApplicationInDebugMode == false) return;
            ConsoleColor savedColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(a.ToString());
            Console.ForegroundColor = savedColor;
        }

        public static void OutPutError<T>(T a)
        {
            if (IsApplicationInDebugMode == false) return;
            ConsoleColor savedColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: " + a.ToString());
            Console.ForegroundColor = savedColor;
        }

        public static void OutPutWarning<T>(T a)
        {
            if (IsApplicationInDebugMode == false) return;
            ConsoleColor savedColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("WARNING: " + a.ToString());
            Console.ForegroundColor = savedColor;
        }

        public static void OutPutAttention<T>(T a)
        {
            if (IsApplicationInDebugMode == false) return;
            ConsoleColor savedColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("ATTENTION: " + a.ToString());
            Console.ForegroundColor = savedColor;
        }
        

        private static int _counter = 1;
        /// <summary>
        /// This method is used only for debugging purpose.
        /// Its prints a tree with a given start treNode
        /// </summary>
        /// <param name="node">Start treeNode to print onwards</param>
        public static void PrintTree(TreeNode node)
        {
            if (IsApplicationInDebugMode == false) return;

            if (_counter == 1)
            {
                Console.WriteLine("============= TREE OUTPUT =============");
            }

            Console.WriteLine("|");
            if (node.Count > 0)
            {
                Console.WriteLine("+ (" + _counter + ") " + String.Concat(Enumerable.Repeat("-", _counter * 3)) + " " + node);

                _counter++;
                foreach (var childNode in node.Children)
                {
                    PrintTree(childNode);
                }
                _counter--;
                return;
            }

            Console.WriteLine("+ (" + _counter + ") " + String.Concat(Enumerable.Repeat("-", _counter * 3)) + " " + node);
        }
    }
}
