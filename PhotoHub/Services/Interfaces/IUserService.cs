using PhotoHub.Models;

namespace PhotoHub.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(UserModel user);
        Task DeleteUserAsync(Guid id);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByIdAsync(Guid id);
        Task UpdateUserAsync(UserModel user);
    }
}