namespace PhotoHub.ViewModels
{
    public class BlogPostDetailsViewModel
    {
        public Guid IdBlogPost { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}
