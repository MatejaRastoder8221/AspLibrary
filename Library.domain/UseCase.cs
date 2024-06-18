using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.domain
{
    public class UseCase : Entity
    {
        public string Name { get; set; }
        public ICollection<UserUseCase> UserUseCases { get; set; } = new HashSet<UserUseCase>();
    }
}
