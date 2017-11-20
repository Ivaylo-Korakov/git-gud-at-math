using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git_Gud_At_Math.Models
{
    class TreeNode : ITreeNode, IEnumerable<TreeNode>
    {
        #region Properties
        public string Value { get; set; }
        public ValueType TypeOfValue { get; set; }
        public List<ITreeNode> Children { get; set; }
        public ITreeNode Parent { get; set; }
        #endregion

        /// <summary>
        /// Constructor
        /// -----------
        /// Inits all the needed properties:
        ///     - value
        ///     - typeOfValue
        ///     - Parent * (see below)
        /// 
        /// Parent functionality:
        ///     - The default value of the parent is null if
        ///       he is not set upon creating the object
        ///     - If there is a value different than null passed as parent:
        ///         - Set the parent with the value
        ///         - Add this new TreeNode as child of this parent
        /// </summary>
        /// <param name="value">The value that this TreeNode holds</param>
        /// <param name="typeOfValue">The type of the value that this TreeNode holds</param>
        /// <param name="parent">The parent of the newly created node (Optional)</param>
        public TreeNode(string value,ValueType typeOfValue, TreeNode parent = null)
        {
            // init
            this.Value = value;
            this.TypeOfValue = typeOfValue;

            this.Children = new List<ITreeNode>();

            if (parent != null)
            {
                this.Parent = parent;
                this.Parent.Children.Add(this);
            }
            else
            {
                this.Parent = null;
            }
        }

        #region Methods
        public void AddChild(string value, ValueType typeOfValue)
        {
            TreeNode newTreeNode = new TreeNode(value, typeOfValue,this);
        }

        public ITreeNode GetChild(string value)
        {
            return this.Children.Select(a => a.Value == value).ToList().First();
        }
        #endregion

        #region IEnumerable
        public IEnumerator<TreeNode> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
