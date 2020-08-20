using FluentValidation;
using SocialMedia.Domain.DTOs;
using System;

namespace SocialMedia.Infrastructure.Validators
{
    public class PostValidator : AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            // Alternative to data annotations
            RuleFor(post => post.Description)
                .NotNull()
                .WithMessage("Description can not be empty.");

            RuleFor(post => post.Description)
                .Length(0, 500)
                .WithMessage("Length must be less than 500 characters.");

            RuleFor(post => post.Date)
                .NotNull()
                .LessThan(DateTime.Now);
        }
    }
}
