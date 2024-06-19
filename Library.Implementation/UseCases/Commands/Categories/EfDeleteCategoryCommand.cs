using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.Categories;
using Library.DataAccess;

namespace Library.Implementation.UseCases.Commands.Categories
{
    public class EfDeleteCategoryCommand : IDeleteCategoryCommand
    {
        private readonly AspContext _context;

        public EfDeleteCategoryCommand(AspContext context)
        {
            _context = context;
        }

        public int Id => 3; // Unique identifier for this use case
        public string Name => "Delete Category Command";
        public string Description => "Deletes an existing category.";

        public void Execute(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                throw new EntityNotFoundException("Category", id);
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
