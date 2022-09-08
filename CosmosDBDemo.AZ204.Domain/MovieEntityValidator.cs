using FluentValidation;

namespace CosmosDBDemo.AZ204.Domain;

public class MovieEntityValidator : AbstractValidator<MovieEntity>
{
    public MovieEntityValidator()
    {
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Year).GreaterThan(1888); // yes, first movie ever done in 1888
        RuleFor(x => x.AgeRestriction).GreaterThan(0);
    }
}