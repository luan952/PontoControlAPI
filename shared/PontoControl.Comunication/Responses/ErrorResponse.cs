using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoControl.Comunication.Responses
{
    public class ErrorResponse
    {
        public List<string> Messages { get; set; }

        public ErrorResponse(string message)
        {
            Messages = new List<string> { message };
        }

        public ErrorResponse(List<string> messages)
        {
            Messages = messages;
        }
    }
}
