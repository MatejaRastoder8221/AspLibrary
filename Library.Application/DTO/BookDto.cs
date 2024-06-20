namespace Library.Application.DTO
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }

    public class SearchBookDto
    {
        public string Keyword { get; set; }
        public string Genre { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? PublishedFrom { get; set; }
        public DateTime? PublishedTo { get; set; }
        public int? Page { get; set; }
        public int? PerPage { get; set; }
    }
}
