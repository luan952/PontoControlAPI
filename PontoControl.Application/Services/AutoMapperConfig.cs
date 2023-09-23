using AutoMapper;
using PontoControl.Comunication.Requests;
using PontoControl.Domain.Entities;

namespace PontoControl.Application.Services
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() 
        {
            EntityForResponse();
            RequestForEntity();
        }

        public void EntityForResponse()
        {
            
        }

        public void RequestForEntity()
        {
            CreateMap<RegisterCollaboratorRequest, Collaborator>();
        }
    }
}
