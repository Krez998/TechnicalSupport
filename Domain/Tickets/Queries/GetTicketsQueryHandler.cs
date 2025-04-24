using AutoMapper;
using DataAccessLayer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Ticket;

namespace Domain.Tickets.Queries
{
    public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, IEnumerable<TicketDto>>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public GetTicketsQueryHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TicketDto>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            var query = _dataContext.Tickets.AsNoTracking().AsQueryable();

            if (request.Status != null)
                query = query.Where(r => r.Status == request.Status);

            if (request.IsShowNotAssigned)
                query = query.Where(r => r.UserTickets.Count == 0);

            var tickets = await query.ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }
    }
}
