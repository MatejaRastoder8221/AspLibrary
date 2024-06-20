using FluentValidation;
using Library.Application.DTO;
using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.Reservations;
using Library.DataAccess;
using Library.domain;

namespace Library.Implementation.UseCases.Commands.Reservations;
public class EfCreateReservationCommand : ICreateReservationCommand
{
    private readonly AspContext _context;
    private readonly IValidator<CreateReservationDto> _validator;

    public EfCreateReservationCommand(AspContext context, IValidator<CreateReservationDto> validator)
    {
        _context = context;
        _validator = validator;
    }

    public int Id => 7;
    public string Name => "Create Reservation Command";
    public string Description => "Creates a new reservation.";

    public void Execute(CreateReservationDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var user = _context.Users.Find(dto.UserId);
        if (user == null)
        {
            throw new EntityNotFoundException("User", dto.UserId);
        }

        var book = _context.Books.Find(dto.BookId);
        if (book == null)
        {
            throw new EntityNotFoundException("Book", dto.BookId);
        }

        var reservation = new Reservation
        {
            UserId = dto.UserId,
            BookId = dto.BookId,
            ReservationDate = dto.ReservationDate,
            Status = dto.Status
        };

        _context.Reservations.Add(reservation);
        _context.SaveChanges();
    }
}
