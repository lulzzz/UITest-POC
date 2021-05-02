using System;

namespace MOF.Etimad.Monafasat.SharedKernel.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public int ErrorCode { get; private set; }

        public object EntityID { get; private set; }

        public EntityNotFoundException() { }

        public EntityNotFoundException(object entityID, string message)
            : base(message)
        {
            this.EntityID = entityID;
        }

        public EntityNotFoundException(object entityID, string message, int errorCode)
            : this(entityID, message)
        {
            this.ErrorCode = errorCode;
        }

        public EntityNotFoundException(string message, Exception inner) : base(message, inner) { }

        public EntityNotFoundException(string message, Exception inner, int errorCode)
            : base(message, inner)
        {
            this.ErrorCode = errorCode;
        }

        protected EntityNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

        protected EntityNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, int errorCode)
            : base(info, context)
        {
            this.ErrorCode = errorCode;
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
