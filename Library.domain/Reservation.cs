using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.domain
{
    public class Reservation : Entity
    {
        public int UserId { get; set; }
        public User User { get; set; } = new User();
        public int BookId { get; set; }
        public Book Book { get; set; } = new Book();
        public DateTime ReservationDate { get; set; }
        public string Status { get; set; } = string.Empty;  // e.g., Pending, Completed, Canceled
    }
}
