namespace PhotoHub.Models
{
    public class ImageModel
    {
        public int IdImage { get; set; }
        public string ?Url { get; set; }
        public Guid IdBlogPost { get; set; }
    }
}
