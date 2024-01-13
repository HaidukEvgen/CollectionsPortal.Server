using CollectionsPortal.Server.DataLayer.Models.Enums;

namespace CollectionsPortal.Server.BusinessLayer.Models.User
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
        public Status Status { get; set; }
    }
}
