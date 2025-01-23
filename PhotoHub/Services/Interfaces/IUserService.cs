using PhotoHub.Models;

namespace PhotoHub.Services.Interfaces
{
    public interface IUserService
    {
        Task Register(UserModel user);
        Task<string> GetUserNameById(Guid id);
        Task<bool> Login(string email, string password);
        Task DeleteUserAsync(Guid id);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByIdAsync(Guid id);
        Task UpdateUserAsync(UserModel user);
        Task<UserModel> GetUserByEmail(string email);
    }
}