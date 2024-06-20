using System;
using System.Collections.Generic;

namespace Library.Application.DTO
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public int CopiesAvailable { get; set; }
        public int PublisherId { get; set; }
        public List<int> AuthorIds { get; set; } = new List<int>();
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}
