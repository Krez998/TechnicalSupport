using MediatR;
using Shared.Models.User;

namespace Domain.Users.Queries
{
    public class GetAgentsQuery : IRequest<IEnumerable<AgentDto>>
    {
    }
}
