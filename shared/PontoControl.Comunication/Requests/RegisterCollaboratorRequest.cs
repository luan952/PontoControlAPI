namespace PontoControl.Comunication.Requests
{
    public class RegisterCollaboratorRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
    }
}
