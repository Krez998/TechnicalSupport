namespace TechnicalSupport.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public Role Role { get; set; }
    }

    public enum Role {
        Disable = -1,
        User = 0,
        Executor = 1,
        Admin = 2
    }

}
