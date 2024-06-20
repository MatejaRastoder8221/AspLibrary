using Implementation.UseCases;
using Library.Application.DTO;
using Library.Application.UseCases.Commands.Books;
using Library.Application.UseCases.Queries.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly UseCaseHandler _useCaseHandler;

        public BooksController(UseCaseHandler useCaseHandler)
        {
            _useCaseHandler = useCaseHandler;
        }

        // GET: api/Books
        [HttpGet]
        public IActionResult Get([FromQuery] SearchBookDto search, [FromServices] IGetBooksQuery query) =>
            Ok(_useCaseHandler.HandleQuery(query, search));

        // GET: api/Books/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetBookQuery query) =>
            Ok(_useCaseHandler.HandleQuery(query, id));

        // POST: api/Books
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateBookDto dto, [FromServices] ICreateBookCommand command)
        {
            _useCaseHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT: api/Books/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateBookDto dto, [FromServices] IUpdateBookCommand command)
        {
            dto.Id = id;
            _useCaseHandler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        // DELETE: api/Books/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteBookCommand command)
        {
            _useCaseHandler.HandleCommand(command, id);
            return StatusCode(204);
        }
    }
}
