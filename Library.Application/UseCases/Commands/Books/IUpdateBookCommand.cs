using Library.Application.DTO;
using Library.application.UseCases;

namespace Library.Application.UseCases.Commands.Books
{
    public interface IUpdateBookCommand : ICommand<UpdateBookDto>
    {
    }
}
