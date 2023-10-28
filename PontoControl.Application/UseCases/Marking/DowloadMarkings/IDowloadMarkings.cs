using PontoControl.Comunication.Requests;

namespace PontoControl.Application.UseCases.Marking.DowloadMarkings
{
    public interface IDowloadMarkings
    {
        Task<MemoryStream> Execute(DowloadMarkingRequest request);
    }
}
