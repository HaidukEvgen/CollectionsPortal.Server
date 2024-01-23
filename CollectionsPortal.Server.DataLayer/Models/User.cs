using CollectionsPortal.Server.DataLayer.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace CollectionsPortal.Server.DataLayer.Models
{
    public class User : IdentityUser
    {
        public DateTime LastLogin { get; set; }
        public Status Status { get; set; }
        public virtual IList<Collection> Collections { get; set; }
    }
}
