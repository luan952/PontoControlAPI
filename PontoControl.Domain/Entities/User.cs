using PontoControl.Domain.Enum;

namespace PontoControl.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType TypeUser { get; set; }
        public bool? IsLogged { get; set; } = false;
    }
}
