using FluentValidation;

namespace Domain.Users.Queries
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(query => query.Id)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}
