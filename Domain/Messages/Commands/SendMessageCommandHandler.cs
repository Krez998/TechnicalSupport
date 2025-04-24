using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Models;
using MediatR;

namespace Domain.Messages.Commands
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public SendMessageCommandHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var message = _mapper.Map<Message>(request);
            await _dataContext.Messages.AddAsync(message, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }
    }
}
