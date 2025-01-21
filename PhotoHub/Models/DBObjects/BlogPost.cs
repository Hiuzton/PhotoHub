namespace PhotoHub.Models.DBObjects
{
    public class BlogPost
    {
        public Guid IdBlogPost { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        public User Author { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
