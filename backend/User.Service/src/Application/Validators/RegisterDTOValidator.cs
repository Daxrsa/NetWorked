using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(t => t.DisplayName)
                    .NotEmpty()
                    .WithMessage("Please specify a display name")
                    .Length(4, 20)
                    .WithMessage("Display name must be between 2 and 20 characters.");
            RuleFor(t => t.Email)
                    .NotEmpty()
                    .WithMessage("Please specify an email address")
                    .EmailAddress()
                    .WithMessage("Invalid email address.");
            RuleFor(t => t.Password).NotEmpty()
                    .WithMessage("Please specify a password")
                    .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z]).{8,}$")
                    .WithMessage("Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.");
            RuleFor(t => t.Username)
                    .NotEmpty()
                    .WithMessage("Please specify a username")
                    .Length(4, 20)
                    .WithMessage("Username must be between 4 and 20 characters.")
                    .Matches("^[a-zA-Z0-9_]*$")
                    .WithMessage("Username can only contain letters, numbers, and underscores.");
        }
    }
}