using FluentValidation;

namespace ShelfLife.Catalog.Application.Sessions.Commands
{
    public class LogReadingSessionsCommandValidator : AbstractValidator<LogReadingSessionCommand>
    {
        public LogReadingSessionsCommandValidator()
        {
            RuleFor(x => x.BookId).NotEqual(Guid.Empty);
            RuleFor(x => x.StartTime).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Start time cannot be in the future.");
            RuleFor(x => x.PagesRead).GreaterThan(0).WithMessage("Pages read must be greater than zero.");
            RuleFor(x => x.MinutesSpent).GreaterThan(0).WithMessage("Minutes spent must be greater than zero.");
        }
    }
}
