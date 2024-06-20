using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.BorrowRecords;
using Library.DataAccess;

namespace Library.Implementation.UseCases.Commands.BorrowRecords;
public class EfDeleteBorrowRecordCommand : IDeleteBorrowRecordCommand
{
    private readonly AspContext _context;

    public EfDeleteBorrowRecordCommand(AspContext context)
    {
        _context = context;
    }

    public int Id => 6;
    public string Name => "Delete Borrow Record Command";
    public string Description => "Deletes an existing borrow record.";

    public void Execute(int id)
    {
        var borrowRecord = _context.BorrowRecords.Find(id);
        if (borrowRecord == null)
        {
            throw new EntityNotFoundException("BorrowRecord", id);
        }

        _context.BorrowRecords.Remove(borrowRecord);
        _context.SaveChanges();
    }
}
