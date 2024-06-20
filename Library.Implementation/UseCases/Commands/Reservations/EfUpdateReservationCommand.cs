using FluentValidation;
using Library.Application.DTO;
using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.Reservations;
using Library.DataAccess;


namespace Library.Implementation.UseCases.Commands.Reservations;

public class EfUpdateReservationCommand : IUpdateReservationCommand
{
    private readonly AspContext _context;
    private readonly IValidator<UpdateReservationDto> _validator;

    public EfUpdateReservationCommand(AspContext context, IValidator<UpdateReservationDto> validator)
    {
        _context = context;
        _validator = validator;
    }

    public int Id => 8;
    public string Name => "Update Reservation Command";
    public string Description => "Updates an existing reservation.";

    public void Execute(UpdateReservationDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var reservation = _context.Reservations.Find(dto.Id);
        if (reservation == null)
        {
            throw new EntityNotFoundException("Reservation", dto.Id);
        }

        reservation.Status = dto.Status;

        _context.SaveChanges();
    }
}
