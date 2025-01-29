using System.Collections.Generic;

namespace PhotoHub.ViewModels
{
    public class HomeViewModel
    {
        public List<BlogPostViewModel> LatestPosts { get; set; }
        public List<UserViewModel> RandomUsers { get; set; }
    }
}

