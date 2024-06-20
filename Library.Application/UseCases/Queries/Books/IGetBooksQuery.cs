using Application.DTO;
using Application.UseCases;
using Library.Application.DTO;

namespace Library.Application.UseCases.Queries.Books
{
    public interface IGetBooksQuery : IQuery<PagedResponse<BookDto>, SearchBookDto>
    {
    }
}
