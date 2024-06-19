using Library.Application.DTO;
using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.Authors;
using Library.DataAccess;
using Library.domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using FluentValidation;


namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AspContext _context;
        private readonly ICreateAuthorCommand _createAuthorCommand;
        private readonly IUpdateAuthorCommand _updateAuthorCommand;
        private readonly IDeleteAuthorCommand _deleteAuthorCommand;

        public AuthorsController(AspContext context,
                                 ICreateAuthorCommand createAuthorCommand,
                                 IUpdateAuthorCommand updateAuthorCommand,
                                 IDeleteAuthorCommand deleteAuthorCommand)
        {
            _context = context;
            _createAuthorCommand = createAuthorCommand;
            _updateAuthorCommand = updateAuthorCommand;
            _deleteAuthorCommand = deleteAuthorCommand;
        }

        // GET: api/Authors/search
        [HttpGet("search")]
        public IActionResult Get([FromQuery] string name)
        {
            IQueryable<Author> query = _context.Authors.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                Debug.WriteLine($"Received name parameter: {name}");
                query = query.Where(a => (a.Name + " " + a.LastName).ToUpper().Contains(name.ToUpper()));
            }

            List<AuthorDto> authors = query.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                LastName = a.LastName,
                Biography = a.Biography,
                BirthDate = a.BirthDate
            }).ToList();

            return Ok(authors);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var author = _context.Authors.Find(id);

            if (author == null)
            {
                return NotFound();
            }

            AuthorDto authorDto = new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                LastName = author.LastName,
                Biography = author.Biography,
                BirthDate = author.BirthDate
            };

            return Ok(authorDto);
        }

        // POST: api/Authors
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateAuthorDto dto)
        {
            try
            {
                _createAuthorCommand.Execute(dto);
                return StatusCode(201);
            }
            catch (FluentValidation.ValidationException ex)
            {
                 return UnprocessableEntity(ex.Errors.Select(e => new { Property = e.PropertyName, Message = e.ErrorMessage
}));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteAuthorCommand.Execute(id);
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

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] UpdateAuthorDto dto)
        {
            try
            {
                dto.Id = id;
                _updateAuthorCommand.Execute(dto);
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
    }
}
