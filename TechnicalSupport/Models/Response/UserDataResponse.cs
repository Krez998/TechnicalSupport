namespace TechnicalSupport.Models.Response
{
    public class UserDataResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public UserType UserType { get; set; }
    }
}
