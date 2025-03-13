using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using TechnicalSupport.Models.UserModels;

namespace TechnicalSupport.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public UserService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task Create(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            await _dataContext.Users.AddAsync(user, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<UserData> Get(int id, CancellationToken cancellationToken)
        {
            var user = await _dataContext.Users
                .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);

            return _mapper.Map<UserData>(user);
        }

        public async Task<IEnumerable<AgentDto>> GetAgents(CancellationToken cancellationToken)
        {
            var agents = await _dataContext.Users.Where(u => u.Role == Role.Agent)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<AgentDto>>(agents);
        }
    }
}
