namespace Domain.Security
{
    public interface IJWTProvider
    {
        public string GetToken(int userId, string login, string role);
    }
}
