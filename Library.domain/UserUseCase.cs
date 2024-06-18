using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.domain
{
    public class UserUseCase
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int UseCaseId { get; set; }
        public UseCase UseCase { get; set; }
    }
}
