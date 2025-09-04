
using Core.Models.Category;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Core.Validators.Category;

public class CategoryCreateValidator : AbstractValidator<CategoryCreateModel>
{
    public CategoryCreateValidator(AppDbContext db)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .Must(name => !string.IsNullOrEmpty(name))
            .WithMessage("Name cannot be empty or null")
            .DependentRules(() =>
            {
                RuleFor(x => x.Name)
                    .MustAsync(async (name, cancellation) =>
                    !await db.Categories.AnyAsync(c => c.Name.ToLower() == name.ToLower().Trim(), cancellation))
                .WithMessage("Category with this name already exists");
            })
            .MaximumLength(250)
            .WithMessage("Name has to be no longer than 250 charachters");
        RuleFor(x => x.Slug)
            .NotEmpty()
            .WithMessage("Slug is required")
            .Must(slug => !string.IsNullOrEmpty(slug))
            .WithMessage("Slug cannot be empty or null")
            .DependentRules(() =>
            {
                RuleFor(x => x.Slug)
                    .MustAsync(async (slug, cancellation) =>
                    !await db.Categories.AnyAsync(c => c.Slug.ToLower() == slug.ToLower().Trim(), cancellation))
                .WithMessage("Category with this slug already exists");
            })
            .MaximumLength(250)
            .WithMessage("Slug has to be no longer than 250 charachters");


        RuleFor(x => x.ImageFile)
            .NotEmpty()
            .WithMessage("Image file is required");
    }
}
