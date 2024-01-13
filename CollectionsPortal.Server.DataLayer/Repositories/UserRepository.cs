using CollectionsPortal.Server.DataLayer.Data;
using CollectionsPortal.Server.DataLayer.Models;
using CollectionsPortal.Server.DataLayer.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CollectionsPortal.Server.DataLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<User> _dbSet;

        public UserRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<User>();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task SetStatusesAsync(List<string> userIds, Status status)
        {
            var usersToUpdate = await _dbSet.Where(u => userIds.Contains(u.Id)).ToListAsync();

            foreach (var user in usersToUpdate)
            {
                user.Status = status;
            }

            await SaveChangesAsync();
        }

        public async Task DeleteUsersAsync(List<string> userIds)
        {
            var usersToDelete = await _dbSet.Where(u => userIds.Contains(u.Id)).ToListAsync();

            _dbSet.RemoveRange(usersToDelete);

            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
