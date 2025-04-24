using FluentValidation;

namespace Domain.Tickets.Queries
{
    public class GetTicketQueryValidator : AbstractValidator<GetTicketQuery>
    {
        public GetTicketQueryValidator()
        {
            RuleFor(query => query.Id)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}
