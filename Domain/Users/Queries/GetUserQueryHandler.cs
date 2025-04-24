using AutoMapper;
using DataAccessLayer;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Models.User;

namespace Domain.Users.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _dataContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException($"Пользователя с идентификатором {request.Id} не существует.");

            return _mapper.Map<UserDto>(user);
        }
    }
}
