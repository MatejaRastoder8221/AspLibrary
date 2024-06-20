using FluentValidation;
using Library.Application.DTO;

namespace Library.API.Validation
{
    public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id is required.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.ISBN).NotEmpty().WithMessage("ISBN is required.");
            RuleFor(x => x.PublicationYear).GreaterThan(0).WithMessage("Publication Year must be greater than 0.");
            RuleFor(x => x.CopiesAvailable).GreaterThanOrEqualTo(0).WithMessage("Copies Available must be greater than or equal to 0.");
            RuleFor(x => x.PublisherId).GreaterThan(0).WithMessage("Publisher Id is required.");
            RuleFor(x => x.AuthorIds).NotEmpty().WithMessage("At least one Author is required.");
            RuleFor(x => x.CategoryIds).NotEmpty().WithMessage("At least one Category is required.");
        }
    }
}
