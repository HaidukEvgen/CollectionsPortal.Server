using CollectionsPortal.Server.DataLayer.Models;

namespace CollectionsPortal.Server.BusinessLayer.Services.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, IList<string> userRoles);
    }
}
