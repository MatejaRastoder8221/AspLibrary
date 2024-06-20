using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.Books;
using Library.DataAccess;
using Library.domain;

namespace Library.Implementation.UseCases.Commands.Books
{
    public class EfDeleteBookCommand : IDeleteBookCommand
    {
        private readonly AspContext _context;

        public EfDeleteBookCommand(AspContext context)
        {
            _context = context;
        }

        public int Id => 5; // Unique identifier for this use case
        public string Name => "Delete Book Command";
        public string Description => "Deletes an existing book.";

        public void Execute(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                throw new EntityNotFoundException("Book", id);
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}
