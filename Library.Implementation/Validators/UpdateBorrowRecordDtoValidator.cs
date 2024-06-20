using FluentValidation;
using Library.Application.DTO;

public class UpdateBorrowRecordDtoValidator : AbstractValidator<UpdateBorrowRecordDto>
{
    public UpdateBorrowRecordDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Borrow Record ID is required.");
    }
}
