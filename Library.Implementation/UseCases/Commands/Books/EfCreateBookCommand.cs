using FluentValidation;
using Library.Application.DTO;
using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.Books;
using Library.DataAccess;
using Library.domain;
using Microsoft.Extensions.Logging;
using System;
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
            _validator.ValidateAndThrow(request);

            var book = new Book
            {
                Title = request.Title,
                ISBN = request.ISBN,
                PublicationYear = request.PublicationYear,
                CopiesAvailable = request.CopiesAvailable,
                PublisherId = request.PublisherId
            };

            // Link existing authors
            book.BookAuthors = request.AuthorIds.Select(authorId => new BookAuthor
            {
                AuthorId = authorId
            }).ToList();

            // Link existing categories
            book.BookCategories = request.CategoryIds.Select(categoryId => new BookCategory
            {
                CategoryId = categoryId
            }).ToList();

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
