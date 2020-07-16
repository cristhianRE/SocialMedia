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
                .Length(0, 1100);

            RuleFor(post => post.Date)
                .NotNull()
                .LessThan(DateTime.Now);
        }
    }
}
