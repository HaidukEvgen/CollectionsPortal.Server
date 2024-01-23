using CollectionsPortal.Server.DataLayer.Data;
using CollectionsPortal.Server.DataLayer.Models;
using CollectionsPortal.Server.DataLayer.Repositories.Interfaces;

namespace CollectionsPortal.Server.DataLayer.Repositories.Implementations
{
    public class CollectionCategoryRepository(AppDbContext context) : BaseRepository<CollectionCategory>(context), ICollectionCategoryRepository
    {
    }
}
