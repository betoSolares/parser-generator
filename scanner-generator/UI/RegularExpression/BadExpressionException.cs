using System;

namespace RegularExpression
{
    [Serializable()]
    public class BadExpressionException : Exception
    {
        public BadExpressionException() : base() { }
        public BadExpressionException(string message) : base(message) { }
        public BadExpressionException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected BadExpressionException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}