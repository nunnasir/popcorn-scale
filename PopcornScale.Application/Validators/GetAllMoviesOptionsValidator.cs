using FluentValidation;
using PopcornScale.Application.Models;

namespace PopcornScale.Application.Validators;

public class GetAllMoviesOptionsValidator : AbstractValidator<GetAllMoviesOptions>
{
    private static readonly string[] AcceptableSortFields =
    {
        "title", "yearofrelease"
    };

    public GetAllMoviesOptionsValidator()
    {
        RuleFor(x => x.YearOfRelease)
            .LessThanOrEqualTo(DateTime.UtcNow.Year);

        RuleFor(x => x.SortField)
            .Must(x => x is null || AcceptableSortFields.Contains(x))
            .WithMessage("You can only sort by 'title' or 'yearofrelease'");
    }
}
