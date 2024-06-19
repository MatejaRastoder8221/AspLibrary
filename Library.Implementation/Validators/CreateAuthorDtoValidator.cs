using FluentValidation;
using Library.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Implementation.Validators
{
    public class CreateAuthorDtoValidator : AbstractValidator<CreateAuthorDto>
    {
        public CreateAuthorDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required.");
            RuleFor(x => x.Biography).NotEmpty().WithMessage("Biography is required.");
            RuleFor(x => x.BirthDate).NotEmpty().WithMessage("BirthDate is required.");
        }
    }
}
