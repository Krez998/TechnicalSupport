namespace TechnicalSupport.Services
{
    public interface IJwtGenerator
    {
        public string Generate(string login, string role);
    }
}
