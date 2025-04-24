using FluentValidation;

namespace Domain.Tickets.Queries
{
    public class GetTicketsQueryValidator : AbstractValidator<GetTicketsQuery>
    {
        public GetTicketsQueryValidator()
        {
            RuleFor(query => query.IsShowNotAssigned)
                .NotNull()
                .Must(value => value == true || value == false)
                .WithMessage("Поле IsShowNotAssigned должно быть true или false.");
        }
    }
}
