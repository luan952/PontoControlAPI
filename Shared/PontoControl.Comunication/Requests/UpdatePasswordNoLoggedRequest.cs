namespace PontoControl.Comunication.Requests
{
    public class UpdatePasswordNoLoggedRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
