using FluentValidation;
using Library.Application.DTO;
using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.Categories;
using Library.DataAccess;
using System.Linq;

namespace Library.Implementation.UseCases.Commands.Categories
{
    public class EfUpdateCategoryCommand : IUpdateCategoryCommand
    {
        private readonly AspContext _context;
        private readonly IValidator<UpdateCategoryDto> _validator;

        public EfUpdateCategoryCommand(AspContext context, IValidator<UpdateCategoryDto> validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 2; // Unique identifier for this use case
        public string Name => "Update Category Command";
        public string Description => "Updates an existing category.";

        public void Execute(UpdateCategoryDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var category = _context.Categories.Find(dto.Id);
            if (category == null)
            {
                throw new EntityNotFoundException("Category", dto.Id);
            }

            category.Name = dto.Name;
            category.Description = dto.Description;

            _context.SaveChanges();
        }
    }
}
