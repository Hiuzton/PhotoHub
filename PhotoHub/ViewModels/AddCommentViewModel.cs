using System.ComponentModel.DataAnnotations;

namespace PhotoHub.ViewModels
{
    public class AddCommentViewModel
    {
        [Required]
        public Guid IdBlogPost { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
        public string Content { get; set; }
    }
}
