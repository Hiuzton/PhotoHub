using System.ComponentModel.DataAnnotations;

namespace PhotoHub.Models
{
    public class ImageModel
    {
        public Guid IdImage { get; set; }

        [Required(ErrorMessage = "URL is required.")]
        public string Url { get; set; }

        public Guid? IdBlogPost { get; set; }
        public BlogPostModel BlogPost { get; set; }
    }
}
