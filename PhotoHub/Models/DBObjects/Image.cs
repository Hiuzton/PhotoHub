namespace PhotoHub.Models.DBObjects
{
    public class Image
    {
        public Guid IdImage { get; set; }
        public string Url { get; set; }
        public Guid? IdBlogPost { get; set; }
        public BlogPostModel BlogPost { get; set; }
    }
}
