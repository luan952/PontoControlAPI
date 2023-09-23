using System.Runtime.Serialization;

namespace PontoControl.Exceptions.ExceptionsBase
{
    public class InvalidLoginException : PontoControlException
    {
        public InvalidLoginException() : base(ResourceMessageError.invalid_login){}

        protected InvalidLoginException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
