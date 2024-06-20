using Application.DTO;
using Library.Application.DTO;
using Library.Application.UseCases.Queries.Books;
using Library.DataAccess;
using Library.domain;
using Library.Implementation.UseCases;
using System;
using System.Linq;

namespace Implementation.UseCases.Queries.Books
{
    public class EfGetBooksQuery : EfUseCase, IGetBooksQuery
    {
        public EfGetBooksQuery(AspContext context)
            : base(context)
        {
        }

        public int Id => 21;

        public string Name => "Search books";

        public string Description => "Search for books with various filters.";

        public PagedResponse<BookDto> Execute(SearchBookDto search)
        {
            IQueryable<Book> query = Context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(b => b.Title.Contains(search.Keyword) || b.BookAuthors.Any(a => a.Author.Name.Contains(search.Keyword)));
            }
            if (!string.IsNullOrEmpty(search.Genre))
            {
                query = query.Where(b => b.BookCategories.Any(c => c.Category.Name == search.Genre));
            }
            if (search.PublishedFrom.HasValue)
            {
                query = query.Where(b => b.PublicationYear >= search.PublishedFrom.Value.Year);
            }
            if (search.PublishedTo.HasValue)
            {
                query = query.Where(b => b.PublicationYear <= search.PublishedTo.Value.Year);
            }

            int totalCount = query.Count();
            int perPage = search.PerPage ?? 10;
            int page = search.Page ?? 1;
            int skip = perPage * (page - 1);

            query = query.Skip(skip).Take(perPage);

            var result = new PagedResponse<BookDto>
            {
                CurrentPage = page,
                PerPage = perPage,
                TotalCount = totalCount,
                Data = query.Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = string.Join(", ", b.BookAuthors.Select(ba => ba.Author.Name)),
                    PublishedDate = new DateTime(b.PublicationYear, 1, 1),
                    Genre = string.Join(", ", b.BookCategories.Select(bc => bc.Category.Name)),
                }).ToList()
            };

            return result;
        }
    }
}
