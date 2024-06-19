using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.Publishers;
using Library.DataAccess;
using Library.domain;

namespace Library.Implementation.UseCases.Commands.Publishers
{
    public class EfDeletePublisherCommand : IDeletePublisherCommand
    {
        private readonly AspContext _context;

        public int Id => 5; // Command Id
        public string Name => "Delete Publisher"; // Command Name
        public string Description => "Deletes an existing publisher"; // Command Description

        public EfDeletePublisherCommand(AspContext context)
        {
            _context = context;
        }

        public void Execute(int request)
        {
            var publisher = _context.Publishers.Find(request);

            if (publisher == null)
            {
                throw new EntityNotFoundException("Publisher", request);
            }

            _context.Publishers.Remove(publisher);
            _context.SaveChanges();
        }
    }
}
