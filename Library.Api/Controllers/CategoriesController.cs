using Library.Application.DTO;
using Library.Application.UseCases.Commands.Categories;
using Library.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Library.Application.Exceptions;
using Library.domain;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AspContext _context;
        private readonly ICreateCategoryCommand _createCategoryCommand;
        private readonly IUpdateCategoryCommand _updateCategoryCommand;
        private readonly IDeleteCategoryCommand _deleteCategoryCommand;

        public CategoriesController(AspContext context,
                                    ICreateCategoryCommand createCategoryCommand,
                                    IUpdateCategoryCommand updateCategoryCommand,
                                    IDeleteCategoryCommand deleteCategoryCommand)
        {
            _context = context;
            _createCategoryCommand = createCategoryCommand;
            _updateCategoryCommand = updateCategoryCommand;
            _deleteCategoryCommand = deleteCategoryCommand;
        }

        // GET: api/Categories/search
        [HttpGet("search")]
        public IActionResult Get([FromQuery] string name)
        {
            IQueryable<Category> query = _context.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                // Log the received name for debugging
                Debug.WriteLine($"Received name parameter: {name}");

                // Perform case-insensitive search for categories containing the name
                query = query.Where(c => c.Name.ToUpper().Contains(name.ToUpper()));
            }

            List<CategoryDto> categories = query.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                // Include other properties or related entities as needed
            }).ToList();

            // Log the number of categories found for debugging
            // Debug.WriteLine($"Found {categories.Count} categories matching the query.");

            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            CategoryDto categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                // Optionally include related entities or child collections here
            };

            return Ok(categoryDto);
        }

        // POST: api/Categories
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateCategoryDto dto)
        {
            try
            {
                _createCategoryCommand.Execute(dto);
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

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteCategoryCommand.Execute(id);
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

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] UpdateCategoryDto dto)
        {
            try
            {
                dto.Id = id;
                _updateCategoryCommand.Execute(dto);
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
    }
}
