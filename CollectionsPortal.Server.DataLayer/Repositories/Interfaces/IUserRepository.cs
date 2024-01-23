using CollectionsPortal.Server.DataLayer.Models;
using CollectionsPortal.Server.DataLayer.Models.Enums;

namespace CollectionsPortal.Server.DataLayer.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllAsync();

        public Task SetStatusesAsync(List<string> userIds, Status status);

        public Task DeleteUsersAsync(List<string> userIds);

        public Task SaveChangesAsync();
    }
}
