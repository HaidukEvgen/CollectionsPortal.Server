using CollectionsPortal.Server.DataLayer.Models;

namespace CollectionsPortal.Server.DataLayer.Repositories.Interfaces
{
    public interface ICollectionItemRepository : IBaseRepository<CollectionItem>
    {
        public Task<IEnumerable<CollectionItem>> GetLatestAsync(int amount);
    }
}