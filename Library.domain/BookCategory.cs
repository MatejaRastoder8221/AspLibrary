using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.domain
{
    public class BookCategory
    {
        public int BookId { get; set; }
        public Book Book { get; set; } = new Book();
        public int CategoryId { get; set; }
        public Category Category { get; set; } = new Category();
    }
}
