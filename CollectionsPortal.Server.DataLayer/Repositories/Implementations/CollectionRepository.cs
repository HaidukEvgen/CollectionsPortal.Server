using CollectionsPortal.Server.DataLayer.Data;
using CollectionsPortal.Server.DataLayer.Models;
using CollectionsPortal.Server.DataLayer.Repositories.Interfaces;

namespace CollectionsPortal.Server.DataLayer.Repositories.Implementations
{
    public class CollectionRepository(AppDbContext context) : BaseRepository<Collection>(context), ICollectionRepository
    {
        public async Task AddItemToCollectionAsync(Collection collection, CollectionItem item)
        {
            collection.Items.Add(item);
            await _context.SaveChangesAsync();
        }
    }
}
