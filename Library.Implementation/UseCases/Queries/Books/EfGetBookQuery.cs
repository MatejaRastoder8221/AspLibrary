using AutoMapper;
using Library.Application.DTO;
using Library.Application.UseCases.Queries.Books;
using Library.DataAccess;
using Library.domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Implementation.UseCases.Queries.Books
{
    public class EfGetBookQuery : EfGenericFindUseCase<BookDto, Book>, IGetBookQuery
    {
        public EfGetBookQuery(AspContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public override int Id => 22;

        public override string Name => "Get book";

        public new string Description => throw new NotImplementedException();

        protected override IQueryable<Book> IncludeRelatedEntities(IQueryable<Book> query)
        {
            return query
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
                .Include(b => b.Publisher)
                .Include(b => b.Reviews).ThenInclude(r => r.User);
        }

        //protected override BookDto MapEntityToDto(Book entity)
        //{
        //    return new BookDto
        //    {
        //        Id = entity.Id,
        //        Title = entity.Title,
        //        Author = string.Join(", ", entity.BookAuthors.Select(ba => ba.Author.Name)),
        //        PublishedDate = new DateTime(entity.PublicationYear, 1, 1),
        //        Genre = string.Join(", ", entity.BookCategories.Select(bc => bc.Category.Name)),
        //    };
        //}
    }
}
