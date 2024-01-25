using AutoMapper;
using CollectionsPortal.Server.BusinessLayer.Exceptions;
using CollectionsPortal.Server.BusinessLayer.Models.Collection;
using CollectionsPortal.Server.BusinessLayer.Models.Item;
using CollectionsPortal.Server.BusinessLayer.Services.Interfaces;
using CollectionsPortal.Server.DataLayer.Models;
using CollectionsPortal.Server.DataLayer.Repositories.Interfaces;

namespace CollectionsPortal.Server.BusinessLayer.Services.Implementations
{
    public class CollectionService : ICollectionService
    {
        private readonly IAzureService _azureService;
        private readonly IUserService _userService;
        private readonly ICollectionRepository _collectionRepository;
        private readonly IMapper _mapper;
        private readonly ICollectionCategoryRepository _collectionCategoryRepository;
        private readonly IItemTagRepository _itemTagRepository;
        private readonly ICollectionItemRepository _collectionItemRepository;

        public CollectionService(
            ICollectionRepository collectionRepository,
            IMapper mapper,
            IAzureService azureService,
            ICollectionCategoryRepository collectionCategoryRepository,
            IUserService userService,
            IItemTagRepository itemTagRepository,
            ICollectionItemRepository collectionItemRepository)
        {
            _collectionRepository = collectionRepository;
            _mapper = mapper;
            _azureService = azureService;
            _collectionCategoryRepository = collectionCategoryRepository;
            _userService = userService;
            _itemTagRepository = itemTagRepository;
            _collectionItemRepository = collectionItemRepository;
        }

        public async Task<IEnumerable<CollectionDto>> GetAllCollectionsAsync()
        {
            var collections = await _collectionRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<CollectionDto>>(collections);
        }

        public async Task<CollectionDto> GetCollectionAsync(int id)
        {
            var collection = await _collectionRepository.GetAsync(id);

            if (collection is null)
            {
                throw new EntityNotFoundException(nameof(Collection), id.ToString());
            }

            return _mapper.Map<CollectionDto>(collection);
        }


        public async Task CreateCollectionAsync(NewCollectionDto createCollectionDto)
        {
            var newCollection = _mapper.Map<Collection>(createCollectionDto);
            newCollection.Creator = await _userService.GetCurrentUserAsync();
            newCollection.Category = await _collectionCategoryRepository.GetAsync(createCollectionDto.CategoryId);
            var imageUrl = await _azureService.UploadImageToAzureBlobStorage(createCollectionDto.Image);
            newCollection.ImageUrl = imageUrl;

            await _collectionRepository.SaveAsync(newCollection);
        }

        public async Task UpdateCollectionAsync(int id, NewCollectionDto updateCollectionDto)
        {
            var existingCollection = await _collectionRepository.GetAsync(id);

            if (existingCollection == null)
            {
                throw new EntityNotFoundException(nameof(Collection), id.ToString());
            }

            var imageUrl = await _azureService.UploadImageToAzureBlobStorage(updateCollectionDto.Image);

            existingCollection = _mapper.Map(updateCollectionDto, existingCollection);
            existingCollection.ImageUrl = imageUrl;
            existingCollection.Category = await _collectionCategoryRepository.GetAsync(updateCollectionDto.CategoryId);
            existingCollection.Creator = await _userService.GetCurrentUserAsync();

            await _collectionRepository.UpdateAsync(existingCollection);
        }

        public async Task DeleteCollectionAsync(int id)
        {
            var collection = await _collectionRepository.GetAsync(id);

            if (collection == null)
            {
                throw new EntityNotFoundException(nameof(Collection), id.ToString());
            }

            await _collectionRepository.RemoveAsync(collection);
        }

        public async Task<bool> IsCollectionOwner(int collectionId, string userId)
        {
            var collection = await _collectionRepository.GetAsync(collectionId);

            return collection.Creator.Id == userId;
        }

        public async Task AddItemToCollectionAsync(int collectionId, NewItemDto newItemDto)
        {
            var collection = await _collectionRepository.GetAsync(collectionId);

            if (collection == null)
            {
                throw new EntityNotFoundException(nameof(Collection), collectionId.ToString());
            }

            var newItem = _mapper.Map<CollectionItem>(newItemDto);

            await CheckForExistingTags(newItem, newItemDto.Tags.Select(tagDto => tagDto.Name));

            await _collectionRepository.AddItemToCollectionAsync(collection, newItem);
        }

        public async Task<IEnumerable<ItemDto>> GetAllCollectionItemsAsync(int collectionId)
        {
            var collection = await _collectionRepository.GetAsync(collectionId);

            if (collection == null)
            {
                throw new EntityNotFoundException(nameof(Collection), collectionId.ToString());
            }

            return _mapper.Map<IEnumerable<ItemDto>>(collection.Items);
        }

        public async Task<ItemDto> GetCollectionItemAsync(int collectionId, int itemId)
        {
            var collection = await _collectionRepository.GetAsync(collectionId);

            if (collection == null)
            {
                throw new EntityNotFoundException(nameof(Collection), collectionId.ToString());
            }

            var item = collection.Items.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
            {
                throw new EntityNotFoundException(nameof(CollectionItem), itemId.ToString());
            }

            return _mapper.Map<ItemDto>(item);
        }

        public async Task UpdateCollectionItemAsync(int collectionId, int itemId, NewItemDto updateItemDto)
        {
            var collection = await _collectionRepository.GetAsync(collectionId);

            if (collection == null)
            {
                throw new EntityNotFoundException(nameof(Collection), collectionId.ToString());
            }

            var excistingItem = collection.Items.FirstOrDefault(i => i.Id == itemId);

            if (excistingItem == null)
            {
                throw new EntityNotFoundException(nameof(CollectionItem), itemId.ToString());
            }

            excistingItem = _mapper.Map(updateItemDto, excistingItem);

            await CheckForExistingTags(excistingItem, updateItemDto.Tags.Select(tagDto => tagDto.Name));

            await _collectionItemRepository.UpdateAsync(excistingItem);
        }

        public async Task<IEnumerable<ItemTagDto>> GetAllTagsAsync()
        {
            var tags = await _itemTagRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ItemTagDto>>(tags);
        }


        public async Task DeleteCollectionItemAsync(int collectionId, int itemId)
        {
            var collection = await _collectionRepository.GetAsync(collectionId);

            if (collection == null)
            {
                throw new EntityNotFoundException(nameof(Collection), collectionId.ToString());
            }

            var excistingItem = collection.Items.FirstOrDefault(i => i.Id == itemId);

            if (excistingItem == null)
            {
                throw new EntityNotFoundException(nameof(CollectionItem), itemId.ToString());
            }

            await _collectionItemRepository.RemoveAsync(excistingItem);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _collectionCategoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        private async Task CheckForExistingTags(CollectionItem newItem, IEnumerable<string> tags)
        {
            var existingTags = await _itemTagRepository.GetTagsByNamesAsync(tags);

            foreach (var tag in tags)
            {
                var existingTag = existingTags.FirstOrDefault(t => t.Name == tag);

                if (existingTag != null)
                {
                    newItem.Tags.Add(existingTag);
                }
                else
                {
                    newItem.Tags.Add(new ItemTag { Name = tag });
                }
            }
        }
    }
}
