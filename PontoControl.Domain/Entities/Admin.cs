namespace PontoControl.Domain.Entities
{
    public class Admin : User
    {
        public List<Collaborator> Collaborators { get; set; }
    }
}
