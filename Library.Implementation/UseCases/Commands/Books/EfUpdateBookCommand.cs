using FluentValidation;
using Library.Application.DTO;
using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.Books;
using Library.DataAccess;
using Library.domain;
using System.Linq;

namespace Library.Implementation.UseCases.Commands.Books
{
    public class EfUpdateBookCommand : IUpdateBookCommand
    {
        private readonly AspContext _context;
        private readonly IValidator<UpdateBookDto> _validator;

        public EfUpdateBookCommand(AspContext context, IValidator<UpdateBookDto> validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 4; // Unique identifier for this use case
        public string Name => "Update Book Command";
        public string Description => "Updates an existing book.";

        public void Execute(UpdateBookDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var book = _context.Books.Find(dto.Id);
            if (book == null)
            {
                throw new EntityNotFoundException("Book", dto.Id);
            }

            book.Title = dto.Title;
            book.ISBN = dto.ISBN;
            book.PublicationYear = dto.PublicationYear;
            book.CopiesAvailable = dto.CopiesAvailable;
            book.PublisherId = dto.PublisherId;

            // Clear existing authors and categories
            book.BookAuthors.Clear();
            book.BookCategories.Clear();

            // Add new authors and categories
            foreach (var authorId in dto.AuthorIds)
            {
                book.BookAuthors.Add(new BookAuthor { AuthorId = authorId });
            }

            foreach (var categoryId in dto.CategoryIds)
            {
                book.BookCategories.Add(new BookCategory { CategoryId = categoryId });
            }

            _context.SaveChanges();
        }
    }
}
