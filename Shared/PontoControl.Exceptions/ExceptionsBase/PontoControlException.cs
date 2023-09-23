using System.Runtime.Serialization;

namespace PontoControl.Exceptions.ExceptionsBase
{
    public class PontoControlException : SystemException
    {
        public PontoControlException(string? message) : base(message)
        {
            
        }

        protected PontoControlException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
