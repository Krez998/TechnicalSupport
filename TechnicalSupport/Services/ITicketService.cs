using TechnicalSupport.Models.TicketModels;
using TechnicalSupport.Models.UserModels;

namespace TechnicalSupport.Services
{
    public interface ITicketService
    {
        Task Create(CreateTicketRequest request, CancellationToken cancellationToken);
        Task<TicketDto> Get(int id, CancellationToken cancellationToken);
        Task<IEnumerable<TicketDto>> GetAll(GetAllTicketsRequest request, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task ChangeTicketStatus(int id, int status, CancellationToken cancellationToken);
        Task SetTicketExecutor(int id, int agentId, CancellationToken cancellationToken);
        Task SendMessage(SendMessageRequest request, CancellationToken cancellationToken);
    }
}
