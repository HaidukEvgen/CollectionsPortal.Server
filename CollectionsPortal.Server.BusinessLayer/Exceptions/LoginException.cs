namespace CollectionsPortal.Server.BusinessLayer.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException() : base("Username or password is incorrect") { }
        public LoginException(string message) : base(message) { }
    }
}
