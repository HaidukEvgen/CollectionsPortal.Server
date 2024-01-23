namespace CollectionsPortal.Server.BusinessLayer.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityType, string id) : base($"Entity of type {entityType} with id = {id}, was not found in database") { }
        public EntityNotFoundException(string message) : base(message) { }
    }
}
