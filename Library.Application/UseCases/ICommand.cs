using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.application.UseCases 
{
    public interface ICommand<TRequest> : IUseCase
    { 
        void Execute(TRequest request);
    }
}
