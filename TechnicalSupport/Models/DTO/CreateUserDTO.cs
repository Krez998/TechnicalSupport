namespace TechnicalSupport.Models.DTO
{
    public class CreateUserDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public Role UserType { get; set; }
    }
}
