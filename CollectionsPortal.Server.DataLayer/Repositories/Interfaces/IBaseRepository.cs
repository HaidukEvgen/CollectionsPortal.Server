using CollectionsPortal.Server.DataLayer.Models;

namespace CollectionsPortal.Server.DataLayer.Repositories.Interfaces
{
    public interface IBaseRepository<DbModel> where DbModel : BaseModel
    {
        public Task<DbModel> GetAsync(int id);

        public Task<IEnumerable<DbModel>> GetAllAsync();

        public Task SaveAsync(DbModel model);

        public Task RemoveAsync(DbModel model);

        public Task UpdateAsync(DbModel model);

        public Task<int> CountAsync();

        public Task SaveRangeAsync(IEnumerable<DbModel> models);

        public Task<bool> AnyAsync();
    }
}
