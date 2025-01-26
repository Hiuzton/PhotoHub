using PhotoHub.Models;

namespace PhotoHub.Services.Interfaces
{
    public interface IImageService
    {
        Task CreateImage(ImageModel imageModel);
        Task<IEnumerable<ImageModel>> GetAllImages();
        Task DeleteImage(Guid id);
        Task<ImageModel> GetImageById(Guid id);
        Task<ImageModel> GetImageByBlogPostId(Guid id);
    }
}