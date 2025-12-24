using FluentValidation;
using LevelUpDev.Application.DTOs.Squads;

namespace LevelUpDev.Application.Validators;

public class CreateSquadRequestValidator : AbstractValidator<CreateSquadRequest>
{
    public CreateSquadRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Squad name is required")
            .MinimumLength(3).WithMessage("Squad name must be at least 3 characters")
            .MaximumLength(30).WithMessage("Squad name must not exceed 30 characters")
            .Matches(@"^[a-zA-Z0-9\s_-]+$")
            .WithMessage("Squad name can only contain letters, numbers, spaces, underscores, and hyphens");

        RuleFor(x => x.Description)
            .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("Description must not exceed 500 characters");

        RuleFor(x => x.Tags)
            .Must(x => x == null || x.Count <= 5)
            .WithMessage("Maximum 5 tags allowed")
            .Must(x => x == null || x.All(t => t.Length <= 20))
            .WithMessage("Each tag must not exceed 20 characters");
    }
}

public class UpdateSquadRequestValidator : AbstractValidator<UpdateSquadRequest>
{
    public UpdateSquadRequestValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3).When(x => !string.IsNullOrEmpty(x.Name))
            .WithMessage("Squad name must be at least 3 characters")
            .MaximumLength(30).When(x => !string.IsNullOrEmpty(x.Name))
            .WithMessage("Squad name must not exceed 30 characters")
            .Matches(@"^[a-zA-Z0-9\s_-]+$").When(x => !string.IsNullOrEmpty(x.Name))
            .WithMessage("Squad name can only contain letters, numbers, spaces, underscores, and hyphens");

        RuleFor(x => x.Description)
            .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("Description must not exceed 500 characters");

        RuleFor(x => x.Tags)
            .Must(x => x == null || x.Count <= 5)
            .WithMessage("Maximum 5 tags allowed");
    }
}
