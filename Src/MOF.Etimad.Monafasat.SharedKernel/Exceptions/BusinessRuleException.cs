using System;

namespace MOF.Etimad.Monafasat.SharedKernal.Exceptions
{
    [Serializable]
    public class BusinessRuleException : Exception
    {
        public int ErrorCode { get; private set; }
        public BusinessRuleException() { }
        public BusinessRuleException(string message) : base(message) { }
        public BusinessRuleException(string message, int errorCode)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }
        public BusinessRuleException(string message, Exception inner) : base(message, inner) { }
        public BusinessRuleException(string message, Exception inner, int errorCode)
            : base(message, inner)
        {
            this.ErrorCode = errorCode;
        }
        protected BusinessRuleException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
    public class AuthorizationException : Exception
    {
        public int ErrorCode { get; private set; }
        public AuthorizationException() { }
        public AuthorizationException(string message) : base(message) { }
        public AuthorizationException(string message, int errorCode)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }
        public AuthorizationException(string message, Exception inner) : base(message, inner) { }
        public AuthorizationException(string message, Exception inner, int errorCode)
            : base(message, inner)
        {
            this.ErrorCode = errorCode;
        }
        protected AuthorizationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
