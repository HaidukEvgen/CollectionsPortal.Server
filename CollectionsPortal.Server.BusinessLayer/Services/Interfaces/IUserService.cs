using CollectionsPortal.Server.BusinessLayer.Models.User;
using CollectionsPortal.Server.DataLayer.Models;
using CollectionsPortal.Server.DataLayer.Models.Enums;

namespace CollectionsPortal.Server.BusinessLayer.Services.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDto>> GetAllAsync();

        public Task RegisterAsync(RegisterUserDto registerUserDto);

        public Task<string> LoginAsync(LoginUserDto loginUserDto);

        public Task SetStatusesAsync(List<string> userIds, Status status);

        public Task DeleteUsersAsync(List<string> userIds);

        public Task<bool> IsUserBanned(string username);

        public Task<bool> IsUserExist(string username);

        public Task<User> GetCurrentUserAsync();
    }
}
