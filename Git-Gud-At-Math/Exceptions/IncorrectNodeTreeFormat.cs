using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Git_Gud_At_Math.Utilities;

namespace Git_Gud_At_Math.Exceptions
{
    public class IncorrectNodeTreeFormat : Exception
    {
        public string IncorrectString { get; private set; }
        public string ErrorMessage { get; private set; }
        public Exception Inner { get; private set; }

        public IncorrectNodeTreeFormat()
        {
        }

        public IncorrectNodeTreeFormat(string message) 
            : base(message)
        {
            this.ErrorMessage = message;
        }

        public IncorrectNodeTreeFormat(string message, string incorrectString)
            : base(message)
        {
            this.ErrorMessage = message;
            this.IncorrectString = incorrectString;
        }

        public IncorrectNodeTreeFormat(string message, Exception inner)
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
