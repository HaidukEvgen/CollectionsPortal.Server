using CollectionsPortal.Server.Api.Attributes;
using CollectionsPortal.Server.BusinessLayer.Models.Collection;
using CollectionsPortal.Server.BusinessLayer.Models.Item;
using CollectionsPortal.Server.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsPortal.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionsController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCollections()
        {
            return Ok(await _collectionService.GetAllCollectionsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCollection([FromRoute] int id)
        {
            return Ok(await _collectionService.GetCollectionAsync(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCollection([FromForm] NewCollectionDto newCollectionDto)
        {
            await _collectionService.CreateCollectionAsync(newCollectionDto);
            return Ok();
        }

        [Authorize(nameof(CollectionAccessForAdminOrCreatorRequirement))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCollection([FromRoute] int id, [FromForm] NewCollectionDto newCollectionDto)
        {
            await _collectionService.UpdateCollectionAsync(id, newCollectionDto);
            return Ok();
        }

        [Authorize(nameof(CollectionAccessForAdminOrCreatorRequirement))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColllection([FromRoute] int id)
        {
            await _collectionService.DeleteCollectionAsync(id);
            return Ok();
        }

        [HttpGet("{collectionId}/items")]
        public async Task<IActionResult> GetAllCollectionItems([FromRoute] int collectionId)
        {
            return Ok(await _collectionService.GetAllCollectionItemsAsync(collectionId));
        }

        [Authorize(nameof(CollectionAccessForAdminOrCreatorRequirement))]
        [HttpPost("{collectionId}/items")]
        public async Task<IActionResult> AddCollectionItem([FromRoute] int collectionId, [FromBody] NewItemDto newItemDto)
        {
            await _collectionService.AddItemToCollectionAsync(collectionId, newItemDto);
            return Ok();
        }

        [Authorize(nameof(CollectionAccessForAdminOrCreatorRequirement))]
        [HttpPut("{collectionId}/items/{itemId}")]
        public async Task<IActionResult> UpdateCollectionItem([FromRoute] int collectionId, [FromRoute] int itemId, [FromBody] NewItemDto newItemDto)
        {
            await _collectionService.UpdateCollectionItemAsync(collectionId, itemId, newItemDto);
            return Ok();
        }

        [Authorize(nameof(CollectionAccessForAdminOrCreatorRequirement))]
        [HttpDelete("{collectionId}/items/{itemId}")]
        public async Task<IActionResult> DeleteCollectionItem([FromRoute] int collectionId, [FromRoute] int itemId)
        {
            await _collectionService.DeleteCollectionItemAsync(collectionId, itemId);
            return Ok();
        }
    }
}
