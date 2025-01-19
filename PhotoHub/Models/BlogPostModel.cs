namespace PhotoHub.Models
{
    public class BlogPostModel
    {
        public Guid IdBlogPost { get; set; }
        public string ?Title { get; set; }
        public string ?Content { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
