using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class CommentDTOValidator : AbstractValidator<CommentDTO>
    {
        public CommentDTOValidator()
        {
            RuleFor(t => t.Body).NotEmpty();
        }
    }
}