namespace PhotoHub.Models.DBObjects
{
    public class Comment
    {
        public Guid IdComment { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid IdBlogPost { get; set; }
        public BlogPost BlogPost { get; set; }
        public Guid IdUser { get; set; }
        public User User { get; set; }
    }
}
