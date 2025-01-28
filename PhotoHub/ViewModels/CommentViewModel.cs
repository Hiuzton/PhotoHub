namespace PhotoHub.ViewModels
{
    public class CommentViewModel
    {
        public Guid IdComment { get; set; }
        public Guid IdAuthor { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
