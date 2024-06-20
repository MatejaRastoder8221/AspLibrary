using Application.UseCases;
using Library.application.UseCases;
using Library.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases
{
    public class UseCaseHandler
    {
        private readonly IApplicationActor _actor;
        private readonly IUseCaseLogger _logger;

        private static List<int> GloballyAllowerd => new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30 };
        public UseCaseHandler(IApplicationActor actor, IUseCaseLogger logger)
        {
            _actor = actor;
            _logger = logger;
        }

        public void HandleCommand<TData>(ICommand<TData> command, TData data)
        {
            HandleCrossCuttingConcerns(command, data);
            command.Execute(data);
        }

        public TResult HandleQuery<TResult, TSearch>(IQuery<TResult, TSearch> query, TSearch search)
            where TResult : class
        {
            HandleCrossCuttingConcerns(query, search);

            TResult result = query.Execute(search);

            return result;
        }
        private void HandleCrossCuttingConcerns(IUseCase useCase, object data)
        {
            if (!_actor.AllowedUseCases.Contains(useCase.Id) && !GloballyAllowerd.Contains(useCase.Id))
            {

                throw new UnauthorizedAccessException();
            }
            _logger.Log(new UseCaseLog
            {
                Username = _actor.Username,
                UseCaseName = useCase.Name,
                UseCaseData = data
            });
        }
    }
}
