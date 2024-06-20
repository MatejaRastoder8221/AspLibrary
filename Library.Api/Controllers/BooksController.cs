using Library.Application.DTO;
using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.Books;
using Library.DataAccess;
using Library.domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentValidation;


namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AspContext _context;
        private readonly ICreateBookCommand _createBookCommand;
        private readonly IUpdateBookCommand _updateBookCommand;
        private readonly IDeleteBookCommand _deleteBookCommand;

        public BooksController(AspContext context,
                               ICreateBookCommand createBookCommand,
                               IUpdateBookCommand updateBookCommand,
                               IDeleteBookCommand deleteBookCommand)
        {
            _context = context;
            _createBookCommand = createBookCommand;
            _updateBookCommand = updateBookCommand;
            _deleteBookCommand = deleteBookCommand;
        }

        // GET: api/Books
        [HttpGet]
        public IActionResult Get()
        {
            var books = _context.Books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationYear = book.PublicationYear,
                CopiesAvailable = book.CopiesAvailable,
                PublisherId = book.PublisherId,
                PublisherName = book.Publisher.Name,
                Authors = book.BookAuthors.Select(ba => ba.Author.Name + " " + ba.Author.LastName).ToList(),
                Categories = book.BookCategories.Select(bc => bc.Category.Name).ToList()
            }).ToList();

            return Ok(books);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _context.Books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationYear = book.PublicationYear,
                CopiesAvailable = book.CopiesAvailable,
                PublisherId = book.PublisherId,
                PublisherName = book.Publisher.Name,
                Authors = book.BookAuthors.Select(ba => ba.Author.Name + " " + ba.Author.LastName).ToList(),
                Categories = book.BookCategories.Select(bc => bc.Category.Name).ToList()
            }).FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST: api/Books
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateBookDto dto)
        {
            try
            {
                _createBookCommand.Execute(dto);
                return StatusCode(201);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return UnprocessableEntity(ex.Errors.Select(e => new { Property = e.PropertyName, Message = e.ErrorMessage }));
            }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message ?? ex.Message;
                return StatusCode(500, new { message = innerExceptionMessage });
            }
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] UpdateBookDto dto)
        {
            try
            {
                dto.Id = id;
                _updateBookCommand.Execute(dto);
                return NoContent();
            }
            catch (FluentValidation.ValidationException ex)
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

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteBookCommand.Execute(id);
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

        // GET: api/Books/search
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string title, [FromQuery] string isbn, [FromQuery] int? year, [FromQuery] string author, [FromQuery] string category)
        {
            IQueryable<Book> query = _context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(b => b.Title.Contains(title));
            }

            if (!string.IsNullOrWhiteSpace(isbn))
            {
                query = query.Where(b => b.ISBN.Contains(isbn));
            }

            if (year.HasValue)
            {
                query = query.Where(b => b.PublicationYear == year.Value);
            }

            if (!string.IsNullOrWhiteSpace(author))
            {
                query = query.Where(b => b.BookAuthors.Any(ba => (ba.Author.Name + " " + ba.Author.LastName).Contains(author)));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(b => b.BookCategories.Any(bc => bc.Category.Name.Contains(category)));
            }

            var books = query.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationYear = book.PublicationYear,
                CopiesAvailable = book.CopiesAvailable,
                PublisherId = book.PublisherId,
                PublisherName = book.Publisher.Name,
                Authors = book.BookAuthors.Select(ba => ba.Author.Name + " " + ba.Author.LastName).ToList(),
                Categories = book.BookCategories.Select(bc => bc.Category.Name).ToList()
            }).ToList();

            return Ok(books);
        }
    }
}
