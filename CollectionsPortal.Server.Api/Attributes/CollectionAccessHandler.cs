using CollectionsPortal.Server.BusinessLayer.Services.Interfaces;
using CollectionsPortal.Server.DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CollectionsPortal.Server.Api.Attributes
{
    public class CollectionAccessHandler : AuthorizationHandler<CollectionAccessForAdminOrCreatorRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICollectionService _collectionService;

        public CollectionAccessHandler(IHttpContextAccessor httpContextAccessor, ICollectionService collectionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _collectionService = collectionService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CollectionAccessForAdminOrCreatorRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return;
            }

            if (context.User.IsInRole(UserRoles.Admin))
            {
                context.Succeed(requirement);
                return;
            }

            var collectionId = GetCollectionIdFromRoute();
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (await _collectionService.IsCollectionOwner(collectionId, userId))
            {
                context.Succeed(requirement);
            }

        }

        private int GetCollectionIdFromRoute()
        {
            var routeValues = _httpContextAccessor.HttpContext?.Request.RouteValues;
            if (routeValues != null && routeValues.TryGetValue("collectionId", out var id))
            {
                if (int.TryParse(id?.ToString(), out var collectionId))
                {
                    return collectionId;
                }
            }

            return 0;
        }
    }

}
