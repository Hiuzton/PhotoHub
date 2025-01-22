using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using PhotoHub.Infrastructure.Interfaces;
using PhotoHub.Models;
using PhotoHub.Models.DBObjects;
using PhotoHub.Repositories.Interfaces;
using PhotoHub.Services.Interfaces;

namespace PhotoHub.Services
{
    public class UserService : IUserService
    {

        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
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

        public async Task Register(UserModel user)
        {
            user.PasswordHash = _passwordHasher.Generate(user.PasswordHash);
            var dbUser = MapToDbModel(user);
            await _userRepository.AddAsync(dbUser);
        }

        public async Task<bool> Login(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);
            return _passwordHasher.Verify(password, user.PasswordHash);
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            return user == null ? null : MapToBusinessModel(user);
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
                PasswordHash = dbUser.PasswordHash,
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
                PasswordHash = user.PasswordHash,
                Role = user.Role
            };
        }
    }
}
