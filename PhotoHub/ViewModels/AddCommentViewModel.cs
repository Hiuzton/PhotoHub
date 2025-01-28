using System.ComponentModel.DataAnnotations;

namespace PhotoHub.ViewModels
{
    public class AddCommentViewModel
    {
        public Guid IdComment { get; set; }
        public Guid IdBlogPost { get; set; }

        [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
        public string Content { get; set; }
    }
}
