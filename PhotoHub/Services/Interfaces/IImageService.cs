using PhotoHub.Models;

namespace PhotoHub.Services.Interfaces
{
    public interface IImageService
    {
        Task CreateImage(ImageModel imageModel);
        Task DeleteImage(Guid id);
        Task<ImageModel> GetImageById(Guid id);
    }
}