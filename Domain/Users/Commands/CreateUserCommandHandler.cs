using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Models;
using MediatR;

namespace Domain.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            await _dataContext.Users.AddAsync(user, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }
    }
}
