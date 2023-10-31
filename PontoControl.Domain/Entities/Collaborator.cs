namespace PontoControl.Domain.Entities
{
    public class Collaborator : User
    {
        public string Position { get; set; }
        public List<Marking> Markings { get; set; }
        public Admin Admin { get; set; }
        public Guid AdminId { get; set; }

    }
}
