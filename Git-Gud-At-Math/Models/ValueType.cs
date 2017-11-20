namespace Git_Gud_At_Math.Models
{
    /// <summary>
    /// This enum will tell us what type is of 
    /// value we have to deal with
    /// </summary>
    public enum ValueType
    {
        Unknown = 0,
        Operator = 1, // Operators like: * / + - 
        Variable = 2, // Variables like: x y z
        Constant = 3  // Constants like: 1 pi 2.78...
    }
}
