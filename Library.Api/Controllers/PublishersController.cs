using Library.Application.DTO;
using Library.Application.UseCases.Commands.Publishers;
using Library.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Library.Application.Exceptions;
using Library.domain;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly AspContext _context;
        private readonly ICreatePublisherCommand _createPublisherCommand;
        private readonly IUpdatePublisherCommand _updatePublisherCommand;
        private readonly IDeletePublisherCommand _deletePublisherCommand;

        public PublishersController(AspContext context,
                                    ICreatePublisherCommand createPublisherCommand,
                                    IUpdatePublisherCommand updatePublisherCommand,
                                    IDeletePublisherCommand deletePublisherCommand)
        {
            _context = context;
            _createPublisherCommand = createPublisherCommand;
            _updatePublisherCommand = updatePublisherCommand;
            _deletePublisherCommand = deletePublisherCommand;
        }

        // GET: api/Publishers/search
        [HttpGet("search")]
        public IActionResult Get([FromQuery] string name)
        {
            IQueryable<Publisher> query = _context.Publishers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                Debug.WriteLine($"Received name parameter: {name}");

                query = query.Where(p => p.Name.ToUpper().Contains(name.ToUpper()));
            }

            List<PublisherDto> publishers = query.Select(p => new PublisherDto
            {
                Id = p.Id,
                Name = p.Name,
                Address = p.Address
            }).ToList();

            Debug.WriteLine($"Found {publishers.Count} publishers matching the query.");

            return Ok(publishers);
        }

        // GET: api/Publishers/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var publisher = _context.Publishers.Find(id);

            if (publisher == null)
            {
                return NotFound();
            }

            PublisherDto publisherDto = new PublisherDto
            {
                Id = publisher.Id,
                Name = publisher.Name,
                Address = publisher.Address
            };

            return Ok(publisherDto);
        }

        // POST: api/Publishers
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreatePublisherDto dto)
        {
            try
            {
                _createPublisherCommand.Execute(dto);
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

        // DELETE: api/Publishers/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                _deletePublisherCommand.Execute(id);
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

        // PUT: api/Publishers/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] UpdatePublisherDto dto)
        {
            try
            {
                dto.Id = id;
                _updatePublisherCommand.Execute(dto);
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
