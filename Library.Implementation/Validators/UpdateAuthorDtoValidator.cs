using FluentValidation;
using Library.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Implementation.Validators
{
    public class UpdateAuthorDtoValidator : AbstractValidator<UpdateAuthorDto>
    {
        public UpdateAuthorDtoValidator()
        {
            Include(new CreateAuthorDtoValidator());
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        }
    }
}
