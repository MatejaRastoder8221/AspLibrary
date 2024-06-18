using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.application.UseCases;

namespace Library.Application
{
    public interface IQuery<TSearch, TResult> : IUseCase
        where TResult : class
    {
        TResult Execute(TSearch search);
    }
}
