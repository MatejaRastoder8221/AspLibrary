using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.Application.DTO;
using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.BorrowRecords;
using Library.DataAccess;
using FluentValidation;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowRecordsController : ControllerBase
    {
        private readonly AspContext _context;
        private readonly ICreateBorrowRecordCommand _createBorrowRecordCommand;
        private readonly IUpdateBorrowRecordCommand _updateBorrowRecordCommand;
        private readonly IDeleteBorrowRecordCommand _deleteBorrowRecordCommand;

        public BorrowRecordsController(AspContext context,
                                       ICreateBorrowRecordCommand createBorrowRecordCommand,
                                       IUpdateBorrowRecordCommand updateBorrowRecordCommand,
                                       IDeleteBorrowRecordCommand deleteBorrowRecordCommand)
        {
            _context = context;
            _createBorrowRecordCommand = createBorrowRecordCommand;
            _updateBorrowRecordCommand = updateBorrowRecordCommand;
            _deleteBorrowRecordCommand = deleteBorrowRecordCommand;
        }

        // GET: api/BorrowRecords
        [HttpGet]
        public IActionResult Get()
        {
            var borrowRecords = _context.BorrowRecords.Select(borrowRecord => new BorrowRecordDto
            {
                Id = borrowRecord.Id,
                UserId = borrowRecord.UserId,
                UserName = borrowRecord.User.Username,
                BookId = borrowRecord.BookId,
                BookTitle = borrowRecord.Book.Title,
                BorrowDate = borrowRecord.BorrowDate,
                ReturnDate = borrowRecord.ReturnDate
            }).ToList();

            return Ok(borrowRecords);
        }

        // GET: api/BorrowRecords/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var borrowRecord = _context.BorrowRecords.Select(borrowRecord => new BorrowRecordDto
            {
                Id = borrowRecord.Id,
                UserId = borrowRecord.UserId,
                UserName = borrowRecord.User.Username,
                BookId = borrowRecord.BookId,
                BookTitle = borrowRecord.Book.Title,
                BorrowDate = borrowRecord.BorrowDate,
                ReturnDate = borrowRecord.ReturnDate
            }).FirstOrDefault(b => b.Id == id);

            if (borrowRecord == null)
            {
                return NotFound();
            }

            return Ok(borrowRecord);
        }

        // POST: api/BorrowRecords
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateBorrowRecordDto dto)
        {
            try
            {
                _createBorrowRecordCommand.Execute(dto);
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

        // PUT: api/BorrowRecords/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] UpdateBorrowRecordDto dto)
        {
            try
            {
                dto.Id = id;
                _updateBorrowRecordCommand.Execute(dto);
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

        // DELETE: api/BorrowRecords/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteBorrowRecordCommand.Execute(id);
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
