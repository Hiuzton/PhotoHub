using System.ComponentModel.DataAnnotations;

namespace PhotoHub.Models
{
    public class CommentModel
    {
        public Guid IdComment { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid IdBlogPost { get; set; }
        public BlogPostModel BlogPost { get; set; }
        public Guid IdAuthor { get; set; }
        public UserModel User { get; set; }
    }
}
