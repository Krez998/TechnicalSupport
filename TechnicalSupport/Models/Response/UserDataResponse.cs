namespace TechnicalSupport.Models.Response
{
    public class UserDataResponse
    {
        public string Token { get; set; }

        public UserAuthDataResponse User {  get; set; }
    }

    public class UserAuthDataResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public Role Role { get; set; }
    }
}
