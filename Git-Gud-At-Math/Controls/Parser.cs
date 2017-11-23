using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Git_Gud_At_Math.Exceptions;
using Git_Gud_At_Math.Models;
using Git_Gud_At_Math.Utilities;
using ValueType = Git_Gud_At_Math.Models.ValueType;

namespace Git_Gud_At_Math.Controls
{
    /// <summary>
    /// The parser class contains all the logic
    /// needed to convert a string into a tree structure
    /// MAIN METHOD:
    ///     void ParseStringToTree(...)
    /// It also proves some utilities that are useful not only 
    /// in parsing the string but in other parts of the program:
    ///     bool IsVariable(...)
    ///     List<string> SplitString(...)
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// This Regex expression returns two groups
        ///     1. The operator in from of the start bracket
        ///     2. The string inside the brackets
        /// </summary>
        public static Regex RegexFunction = new Regex(@"(.{1})\((.*)\)");

        /// <summary>
        /// THE MAIN PARSER METHOD:
        /// Takes string and a tree node (optional)
        ///     1. Uses regex to separate the operator and the content
        ///        between the brackets
        ///     2. The content between the brackets is separated by SplitString(...)
        ///        into one or more strings aka functions 
        ///     3. Repeat with recursion until we hit a state where the regex is no longer
        ///        matchable which means that there is no more stacked functions
        /// </summary>
        /// <param name="input">The string to parse</param>
        /// <param name="rootNode">The start/root node of the tree</param>
        public static void ParseStringToTree(string input, TreeNode rootNode = null)
        {
            try
            {
                if (rootNode == null)
                    rootNode = new TreeNode("Root", ValueType.Unknown);

                Match match = RegexFunction.Match(input);

                if (match.Success)
                {
                    var mathOperator = match.Groups[1].Value;
                    var mainFunction = match.Groups[2].Value;

                    TreeNode newOperatorNode = new TreeNode(mathOperator, ValueType.Operator);
                    rootNode.Add(newOperatorNode);

                    List<string> arguments = SplitString(mainFunction, ',');

                    foreach (var argument in arguments)
                    {
                        Debug.OutPut(argument);
                        ParseStringToTree(argument, newOperatorNode);
                    }
                }
                else
                {
                    List<string> arguments = SplitString(input, ',');

                    foreach (var argument in arguments)
                    {
                        rootNode.Add(IsVariable(argument)
                            ? new TreeNode(argument, ValueType.Variable)
                            : new TreeNode(argument, ValueType.Constant));
                    }
                }
            }
            catch (UnparseableString e)
            {
                e.Print();
                throw;
            }
            catch (Exception)
            {
                Debug.OutPutError("Something unexpected happened and we are not " +
                                  "able to parse the string / function");
                throw;
            }
        }

        /// <summary>
        /// A simple function that checks if a string value 
        /// can be parsed or not therefor works like a variable checker.
        /// </summary>
        /// <param name="input">String to check</param>
        /// <returns>Result of the check</returns>
        public static bool IsVariable(string input)
        {
            double temp;
            if (double.TryParse(input, out temp))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// This function splits a string which includes brackets
        /// by a specified character (splitter) in our case a comma (,)
        /// </summary>
        /// <param name="text">The text to separate</param>
        /// <param name="splitter">The char to separate by</param>
        /// <returns>A list of all the substrings</returns>
        public static List<string> SplitString(string text, char splitter)
        {
            var bracketsLevel = 0;
            var lastPosition = 0;
            var substrings = new List<string>();

            for (var i = 0; i != text.Length; i++)
            {
                if (text[i] == '(')
                {
                    bracketsLevel++;
                }
                else if (text[i] == ')')
                {
                    bracketsLevel--;
                    if (bracketsLevel < 0)
                    {
                        throw new UnparseableString("Unable to parse the string", text);
                    }
                }
                else if (text[i] == splitter)
                {
                    if (bracketsLevel == 0)
                    {
                        substrings.Add(text.Substring(lastPosition, i - lastPosition));
                        lastPosition = i + 1;
                    }
                }
            }

            if (lastPosition != text.Length)
            {
                substrings.Add(text.Substring(lastPosition, text.Length - lastPosition));
            }

            return substrings;
        }

        public static int Counter = 1;
        public static string ExportTree(TreeNode rootNode)
        {
            Counter = 1;
            string result = "graph calculus {\r\n" +
                            "node [ fontname = \"Arial\" ]\r\n" +
                            GetTreeToString(rootNode) +
                            "}\r\n";

            return result;
        }
        
        private static string GetTreeToString(TreeNode rootNode)
        {
            string result = string.Empty;

            result += "node" + rootNode.GetHashCode() + " [ label= \"" + ConvertSymbol(rootNode.Value) + "\" ]\n";

            foreach (var rootNodeChild in rootNode.Children)
            {
                result += "node" + rootNode.GetHashCode() + " -- node" + rootNodeChild.GetHashCode() + "\n";
                result += GetTreeToString(rootNodeChild);
            }

            return result;
        }

        private static string ConvertSymbol(string symbol)
        {
            switch (symbol)
            {
                case "s":
                    return "sin";
                case "c":
                    return "cos";
                case "l":
                    return "ln";
                case "e":
                    return "exp";
                case "p":
                    return "pi";
            }

            return symbol;
        }
    }
}