using FluentValidation;
using Library.Application.DTO;

namespace Library.API.Validation
{
    public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.ISBN).NotEmpty().WithMessage("ISBN is required.");
            RuleFor(x => x.PublicationYear).GreaterThan(0).WithMessage("Publication Year must be greater than 0.");
            RuleFor(x => x.CopiesAvailable).GreaterThanOrEqualTo(0).WithMessage("Copies Available must be non-negative.");
            RuleFor(x => x.PublisherId).NotEmpty().WithMessage("Publisher ID is required.");
            RuleFor(x => x.AuthorIds).NotEmpty().WithMessage("At least one author is required.");
            RuleFor(x => x.CategoryIds).NotEmpty().WithMessage("At least one category is required.");
        }
    }
}
