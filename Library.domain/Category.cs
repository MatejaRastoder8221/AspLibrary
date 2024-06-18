using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.domain
{
    public class Category : NamedEntity
    {
        public string Description { get; set; } = string.Empty;
        public ICollection<BookCategory> BookCategories { get; set; } = new HashSet<BookCategory>();
    }
}
