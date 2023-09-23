namespace PontoControl.Domain.Entities
{
    public class Marking : BaseEntity
    {
        public DateTime Hour { get; set; }
        public Collaborator Collaborator { get; set; }
        public Guid CollaboratorId { get; set; }
    }
}
