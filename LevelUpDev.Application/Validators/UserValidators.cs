using FluentValidation;
using LevelUpDev.Application.DTOs.Users;

namespace LevelUpDev.Application.Validators;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.GitHubId)
            .NotEmpty().WithMessage("GitHub ID is required")
            .MaximumLength(50).WithMessage("GitHub ID must not exceed 50 characters");

        RuleFor(x => x.GitHubUsername)
            .NotEmpty().WithMessage("GitHub username is required")
            .MaximumLength(39).WithMessage("GitHub username must not exceed 39 characters")
            .Matches(@"^[a-zA-Z0-9](?:[a-zA-Z0-9]|-(?=[a-zA-Z0-9])){0,38}$")
            .WithMessage("Invalid GitHub username format");

        RuleFor(x => x.LeetCodeUsername)
            .NotEmpty().WithMessage("LeetCode username is required")
            .MaximumLength(50).WithMessage("LeetCode username must not exceed 50 characters")
            .Matches(@"^[a-zA-Z0-9_-]+$")
            .WithMessage("LeetCode username can only contain letters, numbers, underscores, and hyphens");

        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("Display name is required")
            .MinimumLength(2).WithMessage("Display name must be at least 2 characters")
            .MaximumLength(50).WithMessage("Display name must not exceed 50 characters");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Invalid email address format");

        RuleFor(x => x.AvatarUrl)
            .Must(BeAValidUrl).When(x => !string.IsNullOrEmpty(x.AvatarUrl))
            .WithMessage("Avatar URL must be a valid URL");
    }

    private static bool BeAValidUrl(string? url)
    {
        if (string.IsNullOrEmpty(url)) return true;
        return Uri.TryCreate(url, UriKind.Absolute, out var result)
               && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
    }
}

public class UpdateUserProfileRequestValidator : AbstractValidator<UpdateUserProfileRequest>
{
    public UpdateUserProfileRequestValidator()
    {
        RuleFor(x => x.DisplayName)
            .MinimumLength(2).When(x => !string.IsNullOrEmpty(x.DisplayName))
            .WithMessage("Display name must be at least 2 characters")
            .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.DisplayName))
            .WithMessage("Display name must not exceed 50 characters");

        RuleFor(x => x.LeetCodeUsername)
            .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.LeetCodeUsername))
            .WithMessage("LeetCode username must not exceed 50 characters")
            .Matches(@"^[a-zA-Z0-9_-]+$").When(x => !string.IsNullOrEmpty(x.LeetCodeUsername))
            .WithMessage("LeetCode username can only contain letters, numbers, underscores, and hyphens");

        RuleFor(x => x.ShowcaseBadgeIds)
            .Must(x => x == null || x.Count <= 3)
            .WithMessage("You can only showcase up to 3 badges");
    }
}
