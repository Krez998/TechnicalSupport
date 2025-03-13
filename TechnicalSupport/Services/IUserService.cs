using TechnicalSupport.Models.UserModels;

namespace TechnicalSupport.Services
{
    public interface IUserService
    {
        Task Create(CreateUserRequest request, CancellationToken cancellationToken);
        Task<UserData> Get(int id, CancellationToken cancellationToken);
        Task<IEnumerable<AgentDto>> GetAgents(CancellationToken cancellationToken);
    }
}
