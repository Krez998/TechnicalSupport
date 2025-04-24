using AutoMapper;
using DataAccessLayer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Models.User;

namespace Domain.Users.Queries
{
    public class GetAgentsQueryHandler : IRequestHandler<GetAgentsQuery, IEnumerable<AgentDto>>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public GetAgentsQueryHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AgentDto>> Handle(GetAgentsQuery request, CancellationToken cancellationToken)
        {
            var agents = await _dataContext.Users.Where(u => u.Role == Role.Agent)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<AgentDto>>(agents);
        }
    }
}
