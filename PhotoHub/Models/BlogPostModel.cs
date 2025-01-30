using System.ComponentModel.DataAnnotations;

namespace PhotoHub.Models
{
    public class BlogPostModel
    {
        public Guid IdBlogPost { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid IdAuthor { get; set; }
        public UserModel Author { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
