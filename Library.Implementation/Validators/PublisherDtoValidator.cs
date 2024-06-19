using FluentValidation;
using Library.Application.DTO;

namespace Library.API.Validation
{
    public class CreatePublisherDtoValidator : AbstractValidator<CreatePublisherDto>
    {
        public CreatePublisherDtoValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().MaximumLength(100);
            RuleFor(dto => dto.Address).NotEmpty().MaximumLength(200);
        }
    }

    public class UpdatePublisherDtoValidator : AbstractValidator<UpdatePublisherDto>
    {
        public UpdatePublisherDtoValidator()
        {
            RuleFor(dto => dto.Id).GreaterThan(0);
            RuleFor(dto => dto.Name).NotEmpty().MaximumLength(100);
            RuleFor(dto => dto.Address).NotEmpty().MaximumLength(200);
        }
    }
}