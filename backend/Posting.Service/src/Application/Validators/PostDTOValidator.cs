using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class PostDTOValidator : AbstractValidator<PostDTO>
    {
        public PostDTOValidator()
        {
            RuleFor(t => t.Title)
            .NotEmpty().WithMessage("Please specify a title for your post.")
            .MaximumLength(20).WithMessage("Title cannot be longer than 20 characters.");

            RuleFor(t => t.Description)
            .MaximumLength(500)
            .WithMessage("Description cannot be longer than 500 character.");
        }
    }
}