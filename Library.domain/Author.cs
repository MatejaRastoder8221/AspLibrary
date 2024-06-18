using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.domain
{
    public class Author : Entity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new HashSet<BookAuthor>();
    }
}
