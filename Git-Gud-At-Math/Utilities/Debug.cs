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
        public static bool IsDebugPrintingTrees = true;

        public static void OutPut<T>(T a)
        {
            return;
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
        /// <param name="skipDebugAccess">Adds ability to skip debug options</param>
        public static void PrintTree(TreeNode node, bool skipDebugAccess = false)
        {
            if (skipDebugAccess == false)
            {
                if (IsApplicationInDebugMode == false) return;
                if (IsDebugPrintingTrees == false) return;
            }

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
                    PrintTree(childNode, skipDebugAccess);
                }
                _counter--;
                return;
            }

            Console.WriteLine("+ (" + _counter + ") " + String.Concat(Enumerable.Repeat("-", _counter * 3)) + " " + node);
        }

        public static void Print2DMatrix(double[,] matrix, bool skipDebugAccess)
        {
            if (skipDebugAccess == false)
            {
                if (IsApplicationInDebugMode == false) return;
                if (IsDebugPrintingTrees == false) return;
            }

            for (int row = 0; row < matrix.GetLength(1); row++)
            {
                for (int col = 0; col < matrix.GetLength(0); col++)
                {
                    Console.Write(matrix[row, col] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
