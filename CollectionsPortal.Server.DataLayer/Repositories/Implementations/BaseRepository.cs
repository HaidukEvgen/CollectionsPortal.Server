using CollectionsPortal.Server.DataLayer.Data;
using CollectionsPortal.Server.DataLayer.Models;
using CollectionsPortal.Server.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CollectionsPortal.Server.DataLayer.Repositories.Implementations
{
    public abstract class BaseRepository<DbModel> : IBaseRepository<DbModel> where DbModel : BaseModel
    {
        protected AppDbContext _context;
        protected DbSet<DbModel> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<DbModel>();
        }

        public async Task<int> CountAsync()
            => await _dbSet.CountAsync();

        public virtual async Task<DbModel?> GetAsync(int id)
            => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        public virtual async Task<IEnumerable<DbModel>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public virtual async Task RemoveAsync(DbModel model)
        {
            _dbSet.Remove(model);
            await _context.SaveChangesAsync();
        }

        public virtual async Task SaveAsync(DbModel model)
        {
            await _dbSet.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(DbModel model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
        }

        public virtual async Task SaveRangeAsync(IEnumerable<DbModel> models)
        {
            await _dbSet.AddRangeAsync(models);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync()
         => await _dbSet.AnyAsync();

    }
}
