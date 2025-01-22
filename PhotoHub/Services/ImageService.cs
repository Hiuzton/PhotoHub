using Microsoft.Extensions.Logging.Abstractions;
using PhotoHub.Models;
using PhotoHub.Models.DBObjects;
using PhotoHub.Repositories;
using PhotoHub.Repositories.Interfaces;
using PhotoHub.Services.Interfaces;

namespace PhotoHub.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<ImageModel> GetImageById(Guid id)
        {
            var dbImage = await _imageRepository.GetByIdAsync(id);
            return dbImage == null ? null : MapToBusinessModel(dbImage);
        }

        public async Task<IEnumerable<ImageModel>> GetAllImages()
        {
            var dbUsers = await _imageRepository.GetAllAsync();
            return dbUsers.Select(MapToBusinessModel);
        }
        public async Task CreateImage(ImageModel imageModel)
        {
            var dbImage = MapToDbModel(imageModel);
            await _imageRepository.AddAsync(dbImage);
        }

        public async Task DeleteImage(Guid id)
        {
            await _imageRepository.DeleteAsync(id);
        }

        private ImageModel MapToBusinessModel(Image dbImage)
        {
            return new ImageModel
            {
                IdImage = dbImage.IdImage,
                Url = dbImage.Url,
                IdBlogPost = dbImage.IdBlogPost,
            };
        }

        private Image MapToDbModel(ImageModel imageModel)
        {
            return new Image
            {
                IdImage = imageModel.IdImage,
                Url = imageModel.Url,
                IdBlogPost = imageModel.IdBlogPost,
            };
        }
    }
}
