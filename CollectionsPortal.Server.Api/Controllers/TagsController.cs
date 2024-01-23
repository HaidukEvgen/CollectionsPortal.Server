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
    public class TagsController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public TagsController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            return Ok(await _collectionService.GetAllTagsAsync());
        }
    }
}
