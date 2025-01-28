using System.ComponentModel.DataAnnotations;

namespace PhotoHub.ViewModels
{
    public class EditCommentViewModel
    {
        public Guid IdComment { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Comment content cannot exceed 500 characters.")]
        public string Content { get; set; }

        public Guid IdBlogPost { get; set; }
    }
}
