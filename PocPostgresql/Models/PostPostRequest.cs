namespace PocPostgresql.Models
{
    public class PostPostRequest
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public Guid BlogId { get; set; }
    }
}
