using PontoControl.Domain.Enum;

namespace PontoControl.Comunication.Responses
{
    public class ResponseLogin
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public bool IsFirstLogin { get; set; }
        public string Position { get; set; }
        public string Token { get; set; }
        public UserType TypeUser { get; set; }
        public List<Domain.Entities.Marking> MarkingsOfDay { get; set; }
    }
}
