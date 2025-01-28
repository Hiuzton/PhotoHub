using System.ComponentModel.DataAnnotations;

namespace PhotoHub.ViewModels
{
    public class BlogPostViewModel
    {
        public Guid IdBlogPost { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
        public string ImageUrl { get; set; }
        public string AuthorName { get; set; }
    }
}
