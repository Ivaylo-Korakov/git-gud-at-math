using System;
using Git_Gud_At_Math.Utilities;

namespace Git_Gud_At_Math.Exceptions
{
    public class UnparseableString : Exception
    {
        public string IncorrectString { get; private set; }
        public string ErrorMessage { get; private set; }
        public Exception Inner { get; private set; }

        public UnparseableString()
        {
        }

        public UnparseableString(string message) 
            : base(message)
        {
            this.ErrorMessage = message;
        }

        public UnparseableString(string message,string incorrectString)
            : base(message)
        {
            this.ErrorMessage = message;
            this.IncorrectString = incorrectString;
        }

        public UnparseableString(string message, Exception inner)
            : base(message, inner)
        {
            this.ErrorMessage = message;
            this.Inner = inner;
        }

        public void Print()
        {
            string outPutExceptionString = this.ErrorMessage + " STRING: " + IncorrectString;
            Debug.OutPutError(outPutExceptionString);
        }
    }
}
