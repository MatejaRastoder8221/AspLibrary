using Library.Application;

namespace Library.Api.Core
{
    public interface IExceptionLogger
    {
        Guid Log(Exception ex, IApplicationActor actor);
    }
}

