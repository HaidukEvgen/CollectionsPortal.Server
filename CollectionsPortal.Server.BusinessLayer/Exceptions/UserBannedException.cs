namespace CollectionsPortal.Server.BusinessLayer.Exceptions
{
    public class UserBannedException : Exception
    {
        public const string ErrorMessage = "You are banned from accessing this resource.";

        public UserBannedException() : base(ErrorMessage) { }
        public UserBannedException(string message) : base(message) { }
    }
}
