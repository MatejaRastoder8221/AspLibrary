using Library.Application.Exceptions;
using Library.Application.UseCases.Commands.Authors;
using Library.DataAccess;
using Library.domain;
using System;

namespace Library.Implementation.UseCases.Commands.Authors
{
    public class EfDeleteAuthorCommand : IDeleteAuthorCommand
    {
        private readonly AspContext _context;

        public EfDeleteAuthorCommand(AspContext context)
        {
            _context = context;
        }

        public int Id => 9; // Unique identifier for this use case
        public string Name => "Delete Author Command";
        public string Description => "Deletes an existing author.";

        public void Execute(int id)
        {
            var author = _context.Authors.Find(id);

            if (author == null)
            {
                throw new EntityNotFoundException("Author", id);
            }

            author.IsActive = false;
            _context.SaveChanges();
        }
    }
}
