using System.Runtime.Serialization;

namespace PontoControl.Exceptions.ExceptionsBase
{
    public class ValidatorErrorException : PontoControlException
    {
        public List<string> ErrorMessages { get; set; }
        public ValidatorErrorException(List<string> errorMessages) : base(string.Empty)
        {
            ErrorMessages = errorMessages;
        }

        protected ValidatorErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
