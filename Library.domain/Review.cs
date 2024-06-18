using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.domain
{
    public class Review : Entity
    {
        public int BookId { get; set; }
        public Book Book { get; set; } = new Book();
        public int UserId { get; set; }
        public User User { get; set; } = new User();
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; }
    }
}
