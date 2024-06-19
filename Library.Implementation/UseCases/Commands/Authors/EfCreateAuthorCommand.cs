using FluentValidation;
using Library.Application.DTO;
using Library.Application.UseCases.Commands.Authors;
using Library.DataAccess;
using Library.domain;
using System;

namespace Library.Implementation.UseCases.Commands.Authors
{
    public class EfCreateAuthorCommand : ICreateAuthorCommand
    {
        private readonly AspContext _context;
        private readonly IValidator<CreateAuthorDto> _validator;

        public EfCreateAuthorCommand(AspContext context, IValidator<CreateAuthorDto> validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 7; // Unique identifier for this use case
        public string Name => "Create Author Command";
        public string Description => "Creates a new author.";

        public void Execute(CreateAuthorDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var author = new Author
            {
                Name = dto.Name,
                LastName = dto.LastName,
                Biography = dto.Biography,
                BirthDate = dto.BirthDate,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Authors.Add(author);
            _context.SaveChanges();
        }
    }
}
