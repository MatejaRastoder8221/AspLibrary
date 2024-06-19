namespace Library.Application.DTO
{
    public class PublisherDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class CreatePublisherDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class UpdatePublisherDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
