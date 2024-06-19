using FluentValidation;
using Library.Application.DTO;

namespace Library.API.Validation
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category name is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Category description is required.");
        }
    }

}
