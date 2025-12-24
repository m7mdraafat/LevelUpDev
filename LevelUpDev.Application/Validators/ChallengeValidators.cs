using FluentValidation;
using LevelUpDev.Application.DTOs.Challenges;

namespace LevelUpDev.Application.Validators;

public class CreateDailyChallengeRequestValidator : AbstractValidator<CreateDailyChallengeRequest>
{
    public CreateDailyChallengeRequestValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required")
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow.Date))
            .WithMessage("Challenge date must be today or in the future");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000).When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("Description must not exceed 1000 characters");

        RuleFor(x => x.Difficulty)
            .NotEmpty().WithMessage("Difficulty is required")
            .Must(x => x == "Easy" || x == "Medium" || x == "Hard")
            .WithMessage("Difficulty must be Easy, Medium, or Hard");

        RuleFor(x => x.Points)
            .GreaterThan(0).WithMessage("Points must be greater than 0")
            .LessThanOrEqualTo(100).WithMessage("Points must not exceed 100");

        RuleFor(x => x.BonusPoints)
            .GreaterThanOrEqualTo(0).When(x => x.BonusPoints.HasValue)
            .WithMessage("Bonus points must be 0 or greater")
            .LessThanOrEqualTo(50).When(x => x.BonusPoints.HasValue)
            .WithMessage("Bonus points must not exceed 50");
    }
}

public class CreateCommunityGoalRequestValidator : AbstractValidator<CreateCommunityGoalRequest>
{
    public CreateCommunityGoalRequestValidator()
    {
        RuleFor(x => x.WeekStart)
            .NotEmpty().WithMessage("Week start date is required")
            .Must(BeAMonday).WithMessage("Week start must be a Monday");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

        RuleFor(x => x.TargetValue)
            .GreaterThan(0).WithMessage("Target value must be greater than 0");
    }

    private static bool BeAMonday(DateOnly date)
    {
        return date.DayOfWeek == DayOfWeek.Monday;
    }
}
