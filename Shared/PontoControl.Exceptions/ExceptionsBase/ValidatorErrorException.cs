using System.Runtime.Serialization;

namespace PontoControl.Exceptions.ExceptionsBase
{
    public class ValidatorErrorException : PontoControlException
    {
        public List<string> ErrorMessages { get; set; }
        public ValidatorErrorException(List<string> errorMessages) : base(string.Empty)
        {
            errorMessages = ErrorMessages;
        }

        protected ValidatorErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
