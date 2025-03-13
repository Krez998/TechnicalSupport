using TechnicalSupport.Models.UserModels;

namespace TechnicalSupport.Services
{
    public interface IAccountService
    {
        Task<UserDataDto> Login(string login, string password, CancellationToken cancellationToken);
    }
}
