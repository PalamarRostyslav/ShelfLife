using FluentValidation;

namespace ShelfLife.Catalog.Application.Shelves.Commands
{
    public class CreateShelfCommandValidator : AbstractValidator<CreateShelfCommand>
    {
        public CreateShelfCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Shelf name cannot be empty.");
            RuleFor(x => x.Name).MaximumLength(200).WithMessage("Shelf name cannot exceed 200 characters.");
        }
    }
}
