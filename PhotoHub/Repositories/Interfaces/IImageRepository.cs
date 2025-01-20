using PhotoHub.Models.DBObjects;

namespace PhotoHub.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task AddAsync(Image image);
        Task DeleteAsync(Guid id);
        Task<Image> GetByIdAsync(Guid id);
    }
}