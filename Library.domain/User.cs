using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.domain
{
        public class User : Entity
        {
            public string Username { get; set; } = string.Empty;
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Password { get; set; } = string.Empty; // Changed from PasswordHash to Password
            public string Email { get; set; } = string.Empty;
            public string Role { get; set; } = string.Empty;
            public DateTime BirthDate { get; set; } // Added BirthDate property
            public virtual Image Image { get; set; } // Added Image property

            public ICollection<BorrowRecord> BorrowRecords { get; set; } = new HashSet<BorrowRecord>();
            public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
            public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
            public ICollection<UserUseCase> UserUseCases { get; set; } = new HashSet<UserUseCase>(); // Added UserUseCases collection
        }
}
