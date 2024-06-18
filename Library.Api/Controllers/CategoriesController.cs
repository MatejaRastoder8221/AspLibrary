using Library.Application.DTO;
using Library.Application.UseCases.Commands.Categories;
using Library.DataAccess;
using Library.domain;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.AspNetCore.Authorization; // Add this namespace for [Authorize] attribute

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICreateCategoryCommand _createCategoryCommand;
        private readonly AspContext _context;

        public CategoriesController(ICreateCategoryCommand createCategoryCommand, AspContext context)
        {
            _createCategoryCommand = createCategoryCommand;
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public IActionResult Get()
        {
            var categories = _context.Categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();

            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
                return NotFound();

            var dto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            return Ok(dto);
        }

        // POST: api/Categories
        [Authorize] // Require authorization for creating categories
        [HttpPost]
        public IActionResult Post([FromBody] CreateCategoryDto dto)
        {
            try
            {
                _createCategoryCommand.Execute(dto);
                return StatusCode(201);
            }
            catch (ValidationException ex)
            {
                return UnprocessableEntity(ex.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // DELETE: api/Categories/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
