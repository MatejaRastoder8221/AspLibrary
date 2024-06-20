using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.Reservations;
using Library.DataAccess;


namespace Library.Implementation.UseCases.Commands.Reservations;

public class EfDeleteReservationCommand : IDeleteReservationCommand
{
    private readonly AspContext _context;

    public EfDeleteReservationCommand(AspContext context)
    {
        _context = context;
    }

    public int Id => 9;
    public string Name => "Delete Reservation Command";
    public string Description => "Deletes an existing reservation.";

    public void Execute(int id)
    {
        var reservation = _context.Reservations.Find(id);
        if (reservation == null)
        {
            throw new EntityNotFoundException("Reservation", id);
        }

        _context.Reservations.Remove(reservation);
        _context.SaveChanges();
    }
}
