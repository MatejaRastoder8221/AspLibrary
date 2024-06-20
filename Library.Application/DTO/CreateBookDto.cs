using System;
using System.Collections.Generic;

namespace Library.Application.DTO
{
    public class CreateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public int CopiesAvailable { get; set; } = 0;
        public int PublisherId { get; set; }
        public List<int> AuthorIds { get; set; } = new List<int>();
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}
