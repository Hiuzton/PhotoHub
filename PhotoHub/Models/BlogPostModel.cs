using System.ComponentModel.DataAnnotations;

namespace PhotoHub.Models
{
    public class BlogPostModel
    {
        public Guid IdBlogPost { get; set; }

        [StringLength(50, ErrorMessage = "String too long. (max 50 char.)")]
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "String too long. (max 1000 char.)")]
        public string Content { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
        public UserModel Author { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss.fff}")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
    }
}
