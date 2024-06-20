using FluentValidation;
using Library.Application.DTO;
using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.BorrowRecords;
using Library.DataAccess;
using Library.domain;

namespace Library.Implementation.UseCases.Commands.BorrowRecords;
public class EfCreateBorrowRecordCommand : ICreateBorrowRecordCommand
{
    private readonly AspContext _context;
    private readonly IValidator<CreateBorrowRecordDto> _validator;

    public EfCreateBorrowRecordCommand(AspContext context, IValidator<CreateBorrowRecordDto> validator)
    {
        _context = context;
        _validator = validator;
    }

    public int Id => 13;
    public string Name => "Create Borrow Record Command";
    public string Description => "Creates a new borrow record.";

    public void Execute(CreateBorrowRecordDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var user = _context.Users.Find(dto.UserId);
        if (user == null)
        {
            throw new EntityNotFoundException("User", dto.UserId);
        }

        var book = _context.Books.Find(dto.BookId);
        if (book == null)
        {
            throw new EntityNotFoundException("Book", dto.BookId);
        }

        var borrowRecord = new BorrowRecord
        {
            UserId = dto.UserId,
            BookId = dto.BookId,
            BorrowDate = dto.BorrowDate
        };

        _context.BorrowRecords.Add(borrowRecord);
        _context.SaveChanges();
    }
}
