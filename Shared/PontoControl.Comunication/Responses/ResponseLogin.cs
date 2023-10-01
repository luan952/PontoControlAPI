namespace PontoControl.Comunication.Responses
{
    public class ResponseLogin
    {
        public string Name { get; set; }
        public string Token { get; set; }
        public List<Domain.Entities.Marking> MarkingsOfDay { get; set; }
    }
}
