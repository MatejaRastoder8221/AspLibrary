using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTO
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public int CopiesAvailable { get; set; }
        public int PublisherId { get; set; }
        public List<int> AuthorIds { get; set; }
        public List<int> CategoryIds { get; set; }
    }

    public class UpdateBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public int CopiesAvailable { get; set; }
        public int PublisherId { get; set; }
        public List<int> AuthorIds { get; set; }
        public List<int> CategoryIds { get; set; }
    }

    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public int CopiesAvailable { get; set; }
        public int PublisherId { get; set; }
        public List<AuthorDto> Authors { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }


}
