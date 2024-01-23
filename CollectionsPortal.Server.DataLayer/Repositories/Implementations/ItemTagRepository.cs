using CollectionsPortal.Server.DataLayer.Data;
using CollectionsPortal.Server.DataLayer.Models;
using CollectionsPortal.Server.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CollectionsPortal.Server.DataLayer.Repositories.Implementations
{
    public class ItemTagRepository(AppDbContext context) : BaseRepository<ItemTag>(context), IItemTagRepository
    {
        public async Task<ItemTag?> GetTagByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<ItemTag>> GetTagsByNamesAsync(IEnumerable<string> tagNames)
        {
            return await _dbSet
                .Where(x => tagNames.Contains(x.Name))
                .ToListAsync();
        }

    }
}
