using CollectionsPortal.Server.DataLayer.Data;
using CollectionsPortal.Server.DataLayer.Models;
using CollectionsPortal.Server.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CollectionsPortal.Server.DataLayer.Repositories.Implementations
{
    public class CollectionItemRepository(AppDbContext context) : BaseRepository<CollectionItem>(context), ICollectionItemRepository
    {
        public async Task<IEnumerable<CollectionItem>> GetLatestAsync(int amount)
        {
           return await _dbSet
                        .OrderByDescending(i => i.Id)
                        .Take(amount)
                        .ToListAsync();
        }
    }
}
