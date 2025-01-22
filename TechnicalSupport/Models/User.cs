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
        public UserType UserType { get; set; }
    }

    public enum UserType {
        User,
        Executor,
        Administrator
    }

}
