using FluentValidation;
using Library.Application.DTO;
using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.Authors;
using Library.DataAccess;
using Library.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Implementation.UseCases.Commands.Authors
{
    public class EfUpdateAuthorCommand : IUpdateAuthorCommand
    {
        private readonly AspContext _context;
        private readonly IValidator<UpdateAuthorDto> _validator;

        public EfUpdateAuthorCommand(AspContext context, IValidator<UpdateAuthorDto> validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 8; // Unique identifier for this use case
        public string Name => "Update Author Command";
        public string Description => "Updates an existing author.";

        public void Execute(UpdateAuthorDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var author = _context.Authors.Find(dto.Id);
            if (author == null)
            {
                throw new EntityNotFoundException("Author", dto.Id);
            }

            author.Name = dto.Name;
            author.LastName = dto.LastName;
            author.Biography = dto.Biography;
            author.BirthDate = dto.BirthDate;
            author.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();
        }
    }
}
