using Library.Application;
using Library.DataAccess;
using Library.domain;

namespace Library.Api.Core
{
    public class ConsoleExceptionLogger : IExceptionLogger
    {
        public Guid Log(Exception ex, IApplicationActor actor)
        {
            var id = Guid.NewGuid();
            Console.WriteLine(ex.Message + " ID: " + id);

            return id;
        }
    }

    public class DbExceptionLogger : IExceptionLogger
    {
        private readonly AspContext _aspContext;

        public DbExceptionLogger(AspContext aspContext)
        {
            _aspContext = aspContext;
        }

        public Guid Log(Exception ex, IApplicationActor actor)
        {
            Guid id = Guid.NewGuid();
            //ID, Message, Time, StrackTrace
            ErrorLog log = new()
            {
                ErrorId = id,
                Message = ex.Message,
                StrackTrace = ex.StackTrace,
                Time = DateTime.UtcNow
            };

            //_aspContext.Entry(log).State = EntityState.Added;

            _aspContext.ErrorLogs.Add(log);

            _aspContext.SaveChanges();

            return id;
        }
    }
}
