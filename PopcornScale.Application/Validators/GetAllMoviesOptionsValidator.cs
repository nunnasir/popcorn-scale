using FluentValidation;
using PopcornScale.Application.Models;

namespace PopcornScale.Application.Validators;

public class GetAllMoviesOptionsValidator : AbstractValidator<GetAllMoviesOptions>
{
    public GetAllMoviesOptionsValidator()
    {
        RuleFor(x => x.YearOfRelease)
            .LessThanOrEqualTo(DateTime.UtcNow.Year);
    }
}
