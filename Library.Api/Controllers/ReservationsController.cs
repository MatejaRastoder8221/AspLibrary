using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.Application.DTO;
using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.Reservations;
using Library.DataAccess;
using FluentValidation;
namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly AspContext _context;
        private readonly ICreateReservationCommand _createReservationCommand;
        private readonly IUpdateReservationCommand _updateReservationCommand;
        private readonly IDeleteReservationCommand _deleteReservationCommand;

        public ReservationsController(AspContext context,
                                       ICreateReservationCommand createReservationCommand,
                                       IUpdateReservationCommand updateReservationCommand,
                                       IDeleteReservationCommand deleteReservationCommand)
        {
            _context = context;
            _createReservationCommand = createReservationCommand;
            _updateReservationCommand = updateReservationCommand;
            _deleteReservationCommand = deleteReservationCommand;
        }

        // GET: api/Reservations
        [HttpGet]
        public IActionResult Get()
        {
            var reservations = _context.Reservations.Select(reservation => new ReservationDto
            {
                Id = reservation.Id,
                UserId = reservation.UserId,
                UserName = reservation.User.Username,
                BookId = reservation.BookId,
                BookTitle = reservation.Book.Title,
                ReservationDate = reservation.ReservationDate,
                Status = reservation.Status
            }).ToList();

            return Ok(reservations);
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var reservation = _context.Reservations.Select(reservation => new ReservationDto
            {
                Id = reservation.Id,
                UserId = reservation.UserId,
                UserName = reservation.User.Username,
                BookId = reservation.BookId,
                BookTitle = reservation.Book.Title,
                ReservationDate = reservation.ReservationDate,
                Status = reservation.Status
            }).FirstOrDefault(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        // POST: api/Reservations
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateReservationDto dto)
        {
            try
            {
                _createReservationCommand.Execute(dto);
                return StatusCode(201);
            }
            catch (ValidationException ex)
            {
                return UnprocessableEntity(ex.Errors.Select(e => new { Property = e.PropertyName, Message = e.ErrorMessage }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] UpdateReservationDto dto)
        {
            try
            {
                dto.Id = id;
                _updateReservationCommand.Execute(dto);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return UnprocessableEntity(ex.Errors.Select(e => new { Property = e.PropertyName, Message = e.ErrorMessage }));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteReservationCommand.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
