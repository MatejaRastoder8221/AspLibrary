using Library.Application.DTO;
using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.Publishers;
using Library.DataAccess;
using Library.domain;

namespace Library.Implementation.UseCases.Commands.Publishers
{
    public class EfUpdatePublisherCommand : IUpdatePublisherCommand
    {
        private readonly AspContext _context;

        public int Id => 2; // Command Id
        public string Name => "Update Publisher"; // Command Name
        public string Description => "Updates an existing publisher"; // Command Description

        public EfUpdatePublisherCommand(AspContext context)
        {
            _context = context;
        }

        public void Execute(UpdatePublisherDto request)
        {
            var publisher = _context.Publishers.Find(request.Id);

            if (publisher == null)
            {
                throw new EntityNotFoundException("Publisher", request.Id);
            }

            publisher.Name = request.Name;
            publisher.Address = request.Address;

            _context.SaveChanges();
        }
    }
}
