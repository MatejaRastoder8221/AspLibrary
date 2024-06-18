using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.domain
{
    public class Publisher : NamedEntity
    {
        public string Address { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
