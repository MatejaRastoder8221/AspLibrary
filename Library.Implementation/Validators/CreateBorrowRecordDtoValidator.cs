using FluentValidation;
using Library.Application.DTO;

public class CreateBorrowRecordDtoValidator : AbstractValidator<CreateBorrowRecordDto>
{
    public CreateBorrowRecordDtoValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required.");
        RuleFor(x => x.BookId).NotEmpty().WithMessage("Book ID is required.");
        RuleFor(x => x.BorrowDate).NotEmpty().WithMessage("Borrow Date is required.");
    }
}
