using FluentValidation;
using Library.Application.DTO;

public class CreateReservationDtoValidator : AbstractValidator<CreateReservationDto>
{
    public CreateReservationDtoValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required.");
        RuleFor(x => x.BookId).NotEmpty().WithMessage("Book ID is required.");
        RuleFor(x => x.ReservationDate).NotEmpty().WithMessage("Reservation Date is required.");
        RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required.");
    }
}
