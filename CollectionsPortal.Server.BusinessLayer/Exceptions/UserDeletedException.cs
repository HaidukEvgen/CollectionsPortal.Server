namespace CollectionsPortal.Server.BusinessLayer.Exceptions
{
    public class UserDeletedException : Exception
    {
        public const string ErrorMessage = "Your account had been deleted. Cannot access the resource";

        public UserDeletedException() : base(ErrorMessage) { }
        public UserDeletedException(string message) : base(message) { }
    }
}
