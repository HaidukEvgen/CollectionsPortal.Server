using CollectionsPortal.Server.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsPortal.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CategoriesController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await _collectionService.GetAllCategoriesAsync());
        }
    }
}
