using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application
{
    public interface IApplicationActor
    {
        int Id { get; }
        string Username { get; }
        string Email { get; }
        string FirstName { get; }
        string LastName { get; }
        IEnumerable<int> AllowedUseCases { get; }
    }
}
