using System;
using System.Collections.Generic;
using System.Text;

namespace TechMart.Business.Exceptions
{
    public class InvalidUserInputException : Exception
    {
        public InvalidUserInputException() { }
        public InvalidUserInputException(string message) : base(message) { }
        public InvalidUserInputException(string message, Exception innerException) : base(message, innerException) { }

    }
}
