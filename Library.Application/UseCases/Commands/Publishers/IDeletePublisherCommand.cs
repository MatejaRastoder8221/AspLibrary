using Library.application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Application.UseCases.Commands.Publishers
{
    public interface IDeletePublisherCommand : ICommand<int>
    {
    }
}
