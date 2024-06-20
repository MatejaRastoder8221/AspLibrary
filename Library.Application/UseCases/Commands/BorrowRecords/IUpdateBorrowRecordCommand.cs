using Library.Application.DTO;
using Library.application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Commands.BorrowRecords
{
    public interface IUpdateBorrowRecordCommand : ICommand<UpdateBorrowRecordDto>
    {
    }

}
