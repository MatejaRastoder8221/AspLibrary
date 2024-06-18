using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.application.UseCases
{
    public interface IQuery<TSearch, TResult> : IUseCase
        where TResult : class
    {
        TResult Execute(TSearch search);
    }
}
