using System.Collections.Generic;

namespace Git_Gud_At_Math.Models
{
    public interface ITreeNode
    {   
        void AddChild(string value,ValueType typeOfValue);
        ITreeNode GetChild(string value);
        
        string Value { get; set; }
        ValueType TypeOfValue { get; set; }
        List<ITreeNode> Children { get; set; }
        ITreeNode Parent { get; set; }
    }
}
