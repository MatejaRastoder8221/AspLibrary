using FluentValidation;
using Library.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Implementation.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
                                    .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                                 .EmailAddress().WithMessage("A valid email is required.");
            RuleFor(x => x.BirthDate).NotEmpty().WithMessage("Birthdate is required.")
                                     .LessThan(DateTime.Now).WithMessage("Birthdate must be in the past.");
        }
    }
}
