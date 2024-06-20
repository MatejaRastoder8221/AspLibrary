using Library.application.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public interface IQuery<TResult, Tsearch> : IUseCase
        where TResult : class
    {
        TResult Execute(Tsearch search);
    }
}
