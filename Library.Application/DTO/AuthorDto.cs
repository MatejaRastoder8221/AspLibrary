using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTO
{
    public class CreateAuthorDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public DateTime BirthDate { get; set; }
    }
    public class UpdateAuthorDto : CreateAuthorDto
    {
        public int Id { get; set; }
    }
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
