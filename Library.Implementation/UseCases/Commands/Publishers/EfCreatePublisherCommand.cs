using Library.Application.DTO;
using Library.Application.UseCases.Commands.Publishers;
using Library.DataAccess;
using Library.domain;

namespace Library.Implementation.UseCases.Commands.Publishers
{
    public class EfCreatePublisherCommand : ICreatePublisherCommand
    {
        private readonly AspContext _context;

        public int Id => 4; // Command Id
        public string Name => "Create Publisher"; // Command Name
        public string Description => "Creates a new publisher"; // Command Description

        public EfCreatePublisherCommand(AspContext context)
        {
            _context = context;
        }

        public void Execute(CreatePublisherDto request)
        {
            var publisher = new Publisher
            {
                Name = request.Name,
                Address = request.Address
            };

            _context.Publishers.Add(publisher);
            _context.SaveChanges();
        }
    }
}
