using PhotoHub.Models;
using PhotoHub.Models.DBObjects;
using PhotoHub.Repositories.Interfaces;
using PhotoHub.Services.Interfaces;

namespace PhotoHub.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> GetUserByIdAsync(Guid id)
        {
            var dbUser = await _userRepository.GetByIdAsync(id);
            return dbUser == null ? null : MapToBusinessModel(dbUser);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            var dbUsers = await _userRepository.GetAllAsync();
            return dbUsers.Select(MapToBusinessModel);
        }

        public async Task CreateUserAsync(UserModel user)
        {
            var dbUser = MapToDbModel(user);
            await _userRepository.AddAsync(dbUser);
        }

        public async Task UpdateUserAsync(UserModel user)
        {
            var dbUser = MapToDbModel(user);
            await _userRepository.UpdateAsync(dbUser);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }

        private UserModel MapToBusinessModel(User dbUser)
        {
            return new UserModel
            {
                IdUser = dbUser.IdUser,
                Username = dbUser.Username,
                Email = dbUser.Email,
                Role = dbUser.Role,
            };
        }

        private User MapToDbModel(UserModel user)
        {
            return new User
            {
                IdUser = user.IdUser,
                Username = user.Username,
                Email = user.Email,
                PasswordHash = "",
                Role = user.Role
            };
        }
    }
}
