using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Git_Gud_At_Math.Models
{
    /// <summary>
    /// A propriatary data structure
    /// UNBALANCED TREE
    /// Basically it a simply a node but when two or more get 
    /// connected it becomes a tree :D 
    /// -------------
    /// A node stores:
    ///     1. Value (string)
    ///     2. TypeOfValue (ValueType) 
    ///         // In order to know what is the type of the value
    /// Other things a node stores:
    ///     1. Parent (TreeNode)
    ///     2. Children (List<TreeNode>)
    ///         // Those are for the connections between the nodes
    ///         // Having both parent and children of a node
    ///         // makes it easer to go UP and DOWN the tree
    /// The node also implements IEnumerable BUT it is not working at the moment.
    /// Work in progress.
    /// </summary>
    public class TreeNode : IEnumerable<TreeNode>
    {
        #region Properties
        public string Value { get; set; }
        public ValueType TypeOfValue { get; set; }
        public List<TreeNode> Children { get; set; }
        public TreeNode Parent { get; set; }
        #endregion



        /// <summary>
        /// Constructor
        /// -----------
        /// Inits all the needed properties:
        ///     - value
        ///     - typeOfValue
        /// </summary>
        /// <param name="value">The value that this TreeNode holds</param>
        /// <param name="typeOfValue">The type of the value that this TreeNode holds</param>
        /// <param name="parent">The parent of the newly created node (Optional)</param>
        public TreeNode(string value, ValueType typeOfValue)
        {
            // init
            this.Value = value;
            this.TypeOfValue = typeOfValue;

            this.Children = new List<TreeNode>();
            this.Parent = null;
        }

        #region Methods
        /// <summary>
        /// Make the connection between the other nodes.
        /// Sets the parent of the input node to the current one (this)
        /// Adds the input node to the list of children of the parent node.
        /// </summary>
        /// <param name="nodeToAdd">The node to add to this one</param>
        public void Add(TreeNode nodeToAdd)
        {
            nodeToAdd.Parent = this;
            this.Children.Add(nodeToAdd);
        }

        /// <summary>
        /// Gets the FIRST child with the value specified
        /// </summary>
        /// <param name="value">Desired Value of the tree node</param>
        /// <returns>TreeNode with that value</returns>
        public TreeNode GetChild(string value)
        {
            return this.Children.First(a => a.Value == value);
        }

        /// <summary>
        /// Because of the nature of objects in C# they pass by reference
        /// In some parts of the core logic we are altering the tree structure
        /// but we also need the original.
        /// The clone functionality will help with that.
        /// ==============================================
        /// Transfer the information of the current node into a new one
        /// If there are children do the same for them and
        /// add the cloned children to the new node and return it.
        /// </summary>
        /// <returns>The start node of the new / same tree</returns>
        public TreeNode Clone()
        {
            TreeNode newNode = new TreeNode(this.Value, this.TypeOfValue);

            if (this.Children.Count > 0)
            {
                foreach (var child in this.Children)
                {
                    newNode.Children.Add(child.Clone());
                }
            }

            return newNode;
        }

        /// <summary>
        /// This is for ease of use
        /// </summary>
        /// <returns>A string to print maybe :D</returns>
        public override string ToString()
        {
            string informationString = "Value: " + this.Value + " | " + this.TypeOfValue;
            return informationString;
        }

        /// <summary>
        /// Counts the children and returns an int
        /// </summary>
        public int Count => this.Children.Count;
        #endregion

        #region IEnumerable
        public IEnumerator<TreeNode> GetEnumerator()
        {
            return this.Children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
