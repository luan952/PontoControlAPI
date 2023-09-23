namespace PontoControl.Domain.Entities
{
    public class Collaborator : User
    {
        public string Position { get; set; }
        public List<Marking> Markings { get; set; }
    }
}
