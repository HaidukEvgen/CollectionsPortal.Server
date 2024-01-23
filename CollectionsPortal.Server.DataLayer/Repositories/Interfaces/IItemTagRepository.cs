using CollectionsPortal.Server.DataLayer.Models;

namespace CollectionsPortal.Server.DataLayer.Repositories.Interfaces
{
    public interface IItemTagRepository : IBaseRepository<ItemTag>
    {
        public Task<ItemTag?> GetTagByNameAsync(string name);

        public Task<IEnumerable<ItemTag>> GetTagsByNamesAsync(IEnumerable<string> tagNames);
    }
}