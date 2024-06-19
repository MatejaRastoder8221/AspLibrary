namespace Library.Application.DTO
{
    public class CategorySearch
    {
        public bool? WithBooks { get; set; }  // Example: Whether to include categories with associated books
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
