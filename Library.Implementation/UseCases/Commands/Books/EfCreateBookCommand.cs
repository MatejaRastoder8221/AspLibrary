using FluentValidation;
using Library.Application.DTO;
using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.Books;
using Library.DataAccess;
using Library.domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Library.Implementation.UseCases.Commands.Books
{
    public class EfCreateBookCommand : ICreateBookCommand
    {
        private readonly AspContext _context;
        private readonly IValidator<CreateBookDto> _validator;
        private readonly ILogger<EfCreateBookCommand> _logger;

        public EfCreateBookCommand(AspContext context, IValidator<CreateBookDto> validator, ILogger<EfCreateBookCommand> logger)
        {
            _context = context;
            _validator = validator;
            _logger = logger;
        }

        public int Id => 4; // Unique identifier for this use case
        public string Name => "Create Book Command";
        public string Description => "Creates a new book.";

        public void Execute(CreateBookDto request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            _logger.LogInformation("Creating a new book with Title: {Title}, ISBN: {ISBN}", request.Title, request.ISBN);

            var book = new Book
            {
                Title = request.Title,
                ISBN = request.ISBN,
                PublicationYear = request.PublicationYear,
                CopiesAvailable = request.CopiesAvailable,
                PublisherId = request.PublisherId
            };

            foreach (var authorId in request.AuthorIds)
            {
                var author = _context.Authors.Find(authorId);
                if (author == null)
                {
                    throw new EntityNotFoundException("Author", authorId);
                }

                _logger.LogInformation("Adding author with ID: {AuthorId}, Name: {AuthorName}, LastName: {AuthorLastName}", authorId, author.Name, author.LastName);

                _context.Entry(author).State = EntityState.Unchanged; // Ensure author is treated as an existing entity

                book.BookAuthors.Add(new BookAuthor
                {
                    AuthorId = authorId,
                    Author = author,
                    Book = book
                });
            }

            foreach (var categoryId in request.CategoryIds)
            {
                var category = _context.Categories.Find(categoryId);
                if (category == null)
                {
                    throw new EntityNotFoundException("Category", categoryId);
                }

                _logger.LogInformation("Adding category with ID: {CategoryId}, Name: {CategoryName}", categoryId, category.Name);

                _context.Entry(category).State = EntityState.Unchanged; // Ensure category is treated as an existing entity

                book.BookCategories.Add(new BookCategory
                {
                    CategoryId = categoryId,
                    Category = category,
                    Book = book
                });
            }

            _context.Books.Add(book);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the book.");
                throw;
            }
        }
    }
}
