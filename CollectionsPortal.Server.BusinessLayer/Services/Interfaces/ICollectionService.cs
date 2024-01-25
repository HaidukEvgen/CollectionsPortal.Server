using CollectionsPortal.Server.BusinessLayer.Models.Collection;
using CollectionsPortal.Server.BusinessLayer.Models.Item;

namespace CollectionsPortal.Server.BusinessLayer.Services.Interfaces
{
    public interface ICollectionService
    {
        public Task<IEnumerable<CollectionDto>> GetAllCollectionsAsync();
        public Task<CollectionDto> GetCollectionAsync(int id);
        public Task CreateCollectionAsync(NewCollectionDto createCollectionDto);
        public Task UpdateCollectionAsync(int id, NewCollectionDto updateCollectionDto);
        public Task DeleteCollectionAsync(int id);
        public Task<bool> IsCollectionOwner(int collectionId, string userId);
        public Task AddItemToCollectionAsync(int collectionId, NewItemDto newItemDto);
        Task<IEnumerable<ItemDto>> GetAllCollectionItemsAsync(int collectionId);
        Task<ItemDto> GetCollectionItemAsync(int collectionId, int itemId);
        public Task UpdateCollectionItemAsync(int collectionId, int itemId, NewItemDto updateItemDto);
        public Task<IEnumerable<ItemTagDto>> GetAllTagsAsync();
        public Task DeleteCollectionItemAsync(int collectionId, int itemId);
    }
}
