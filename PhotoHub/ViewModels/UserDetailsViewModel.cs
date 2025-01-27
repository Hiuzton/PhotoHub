using PhotoHub.Models;

namespace PhotoHub.ViewModels
{
    public class UserDetailsViewModel
    {
        public string Username { get; set; }
        public IEnumerable<ImageModel> Images { get; set; }
    }
}
