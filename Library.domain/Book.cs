using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.domain
{
    public class Book : Entity
    {
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public int CopiesAvailable { get; set; } = 0;
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; } = new Publisher();
        public ICollection<BookAuthor> BookAuthors { get; set; } = new HashSet<BookAuthor>();
        public ICollection<BookCategory> BookCategories { get; set; } = new HashSet<BookCategory>();
        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new HashSet<BorrowRecord>();
        public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}
