using Application.UseCases;
using Library.Application.DTO;

namespace Library.Application.UseCases.Queries.Books
{
    public interface IGetBookQuery : IQuery<BookDto, int>
    {
    }
}
