using System.ComponentModel.DataAnnotations;

namespace PhotoHub.Models
{
    public class CommentModel
    {
        public Guid IdComment { get; set; }

        [StringLength(250, ErrorMessage = "String too long. (max 250 char.)")]
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [Required]
        public Guid IdBlogPost { get; set; }
        public BlogPostModel BlogPost { get; set; }

        [Required]
        public Guid IdUser { get; set; }
        public UserModel User { get; set; }
    }
}
