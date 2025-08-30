
using Core.Models.Category;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Core.Validators.Category;

public class CategoryEditValidator : AbstractValidator<CategoryEditModel>
{
    public CategoryEditValidator(AppDbContext db)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .Must(name => !string.IsNullOrEmpty(name))
            .WithMessage("Name cannot be empty or null")
            .DependentRules(() =>
            {
                RuleFor(x => x.Name)
                    .MustAsync(async (model, name, cancellationToken) =>
                    !await db.Categories.AnyAsync(x => (x.Name.ToLower() == name.ToLower().Trim() && x.Id != model.Id), cancellationToken))
                    .WithMessage("Category with this name already exists");
            })

            .MaximumLength(250)
            .WithMessage("Name has to be no longer than 250 charachters");

        RuleFor(x => x.Slug)
            .NotEmpty()
            .WithMessage("Slug is required")
            .MaximumLength(250)
            .WithMessage("Slug has to be no longer than 250 charachters");

    }
}
