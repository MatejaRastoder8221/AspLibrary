using FluentValidation;
using Library.API.Validation;
using Library.Application.DTO;
using Library.Application.UseCases.Commands.Categories;
using Library.DataAccess;
using Library.domain;

namespace Library.Implementation.UseCases.Commands.Categories
{
    public class EfCreateCategoryCommand : EfUseCase, ICreateCategoryCommand
    {
        private readonly CreateCategoryDtoValidator _validator;

        public EfCreateCategoryCommand(AspContext context, CreateCategoryDtoValidator validator)
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 1;

        public string Name => "CreateCategory";

        public string Description => "Creates a new category in the system.";

        public void Execute(CreateCategoryDto request)
        {
            _validator.ValidateAndThrow(request);

            var category = new Category
            {
                Name = request.Name,
                Description = request.Description
            };

            Context.Categories.Add(category);
            Context.SaveChanges();
        }
    }
}
