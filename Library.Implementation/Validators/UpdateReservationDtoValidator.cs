using FluentValidation;
using Library.Application.DTO;

public class UpdateReservationDtoValidator : AbstractValidator<UpdateReservationDto>
{
    public UpdateReservationDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Reservation ID is required.");
        RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required.");
    }
}
