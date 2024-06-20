using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTO
{
    public class UpdateBorrowRecordDto
    {
        public int Id { get; set; }
        public DateTime? ReturnDate { get; set; }
    }

}
