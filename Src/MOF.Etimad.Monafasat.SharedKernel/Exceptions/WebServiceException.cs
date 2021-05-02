using System;

namespace MOF.Etimad.Monafasat.SharedKernel.Exceptions
{
    [Serializable]
    public class WebServiceException : Exception
    {
        public int ErrorCode { get; private set; }
        public WebServiceException() { }
        public WebServiceException(string message) : base(message) { }
        public WebServiceException(string message, int errorCode)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }

        public WebServiceException(string message, Exception inner) : base(message, inner) { }
        public WebServiceException(string message, Exception inner, int errorCode)
            : base(message, inner)
        {
            this.ErrorCode = errorCode;
        }

        protected WebServiceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        protected WebServiceException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context, int errorCode)
            : base(info, context) { this.ErrorCode = errorCode; }
    }
}
