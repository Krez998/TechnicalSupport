using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using TechnicalSupport.Models.TicketModels;

namespace TechnicalSupport.Services
{
    public class TicketService : ITicketService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public TicketService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task Create(CreateTicketRequest request, CancellationToken cancellationToken)
        {
            var ticket = _mapper.Map<Ticket>(request);
            ticket.AgentId = 1;
            await _dataContext.Tickets.AddAsync(ticket, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var ticket = await _dataContext.Tickets
                .AsNoTracking()
                .FirstOrDefaultAsync(ticket => ticket.Id == id, cancellationToken) ?? throw new KeyNotFoundException($"Заявка с идентификатором {id} не найдена");

            _dataContext.Tickets.Remove(ticket);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<TicketDto> Get(int id, CancellationToken cancellationToken)
        {
            var ticketQuery = await _dataContext.Tickets
                .AsNoTracking()
                .FirstOrDefaultAsync(ticket => ticket.Id == id, cancellationToken) ?? throw new KeyNotFoundException($"Заявка с идентификатором {id} не найдена");

            var userFullName = await _dataContext.Users
                .AsNoTracking()
                .Where(u => u.Id == ticketQuery.UserId)
                .Select(x => new { FullName = $"{x.LastName} {x.FirstName} {x.Patronymic}".Trim() })
                .FirstOrDefaultAsync(cancellationToken) ?? throw new KeyNotFoundException($"Не найдено фио пользователя с идентификатором {id}");

            var agentFullName = await _dataContext.Users
                .AsNoTracking()
                .Where(u => u.Id == ticketQuery.AgentId)
                .Select(x => new { FullName = $"{x.LastName} {x.FirstName} {x.Patronymic}".Trim() })
                .FirstOrDefaultAsync(cancellationToken);

            var ticket = _mapper.Map<TicketDto>(ticketQuery);

            ticket.UserFullName = userFullName.FullName;
            ticket.AgentFullName = agentFullName?.FullName;

            ticket.Messages = await
                (from t in _dataContext.Tickets.AsNoTracking()
                 join c in _dataContext.Chats.AsNoTracking() on t.ChatId equals c.Id
                 join m in _dataContext.Messages.AsNoTracking() on c.Id equals m.ChatId
                 where ticketQuery.Id == t.Id
                 select new
                 {
                     id = m.Id,
                     senderId = m.SenderId,
                     datetime = m.CreatedAt,
                     content = m.Content
                 })
                 .ToListAsync(cancellationToken);

            return ticket;
        }

        public async Task<IEnumerable<TicketDto>> GetAll(GetAllTicketsRequest request, CancellationToken cancellationToken)
        {
            var tickets = await _dataContext.Tickets
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            tickets = request.Status == null ? tickets : tickets.Where(r => r.Status != (TicketStatus)request.Status).ToList();

            if (request.IsShowNotAssigned)
                tickets = tickets.Where(r => r.AgentId == null).ToList();

            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }

        public async Task ChangeTicketStatus(int id, int status, CancellationToken cancellationToken)
        {
            var ticket = await _dataContext.Tickets
                .AsNoTracking()
                .FirstOrDefaultAsync(ticket => ticket.Id == id, cancellationToken) ?? throw new KeyNotFoundException($"Заявка с идентификатором {id} не найдена");

            ticket.Status = (TicketStatus)status;

            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task SetTicketExecutor(int id, int agentId, CancellationToken cancellationToken)
        {
            var ticket = await _dataContext.Tickets
                .AsNoTracking()
                .FirstOrDefaultAsync(ticket => ticket.Id == id, cancellationToken) ?? throw new KeyNotFoundException($"Заявка с идентификатором {id} не найдена");

            var agent = await _dataContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == id, cancellationToken) ?? throw new KeyNotFoundException($"Пользователь с идентификатором {id} не существует");

            ticket.AgentId = agent.Id;
            ticket.Status = TicketStatus.Assigned;

            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task SendMessage(SendMessageRequest request, CancellationToken cancellationToken)
        {
            var message = _mapper.Map<Message>(request);
            message.CreatedAt = DateTime.UtcNow;
            message.UpdatedAt = DateTime.UtcNow;
            await _dataContext.Messages.AddAsync(message, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }
    }
}
