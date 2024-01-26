using CollectionsPortal.Server.DataLayer.Models;

namespace CollectionsPortal.Server.DataLayer.Repositories.Interfaces
{
    public interface ICollectionRepository : IBaseRepository<Collection>
    {
        public Task AddItemToCollectionAsync(Collection collection, CollectionItem item);
        public Task<IEnumerable<Collection>> GetBiggestAsync(int amount);
    }
}
