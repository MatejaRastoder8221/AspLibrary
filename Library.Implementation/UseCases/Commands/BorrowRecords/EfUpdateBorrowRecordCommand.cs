using FluentValidation;
using Library.Application.DTO;
using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.BorrowRecords;
using Library.DataAccess;

namespace Library.Implementation.UseCases.Commands.BorrowRecords;
public class EfUpdateBorrowRecordCommand : IUpdateBorrowRecordCommand
{
    private readonly AspContext _context;
    private readonly IValidator<UpdateBorrowRecordDto> _validator;

    public EfUpdateBorrowRecordCommand(AspContext context, IValidator<UpdateBorrowRecordDto> validator)
    {
        _context = context;
        _validator = validator;
    }

    public int Id => 5;
    public string Name => "Update Borrow Record Command";
    public string Description => "Updates an existing borrow record.";

    public void Execute(UpdateBorrowRecordDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var borrowRecord = _context.BorrowRecords.Find(dto.Id);
        if (borrowRecord == null)
        {
            throw new EntityNotFoundException("BorrowRecord", dto.Id);
        }

        borrowRecord.ReturnDate = dto.ReturnDate;

        _context.SaveChanges();
    }
}
