using System;

namespace MOF.Etimad.Monafasat.SharedKernel.Exceptions
{
    [Serializable]
    public class UnHandledAccessException : Exception
    {
        public int ErrorCode { get; private set; }
        public UnHandledAccessException() { }
        public UnHandledAccessException(string message) : base(message) { }
        public UnHandledAccessException(string message, int errorCode)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }
        public UnHandledAccessException(string message, Exception inner) : base(message, inner) { }
        public UnHandledAccessException(string message, Exception inner, int errorCode)
            : base(message, inner)
        {
            this.ErrorCode = errorCode;
        }
        protected UnHandledAccessException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
