using PhotoHub.Models.DBObjects;

namespace PhotoHub.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
    }
}