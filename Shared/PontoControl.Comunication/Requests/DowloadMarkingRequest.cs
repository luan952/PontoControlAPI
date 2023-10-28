using PontoControl.Comunication.Responses;

namespace PontoControl.Comunication.Requests
{
    public class DowloadMarkingRequest
    {
        public List<GetMarkingResponse> ListMarkings { get; set; }
    }
}
