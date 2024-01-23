namespace CollectionsPortal.Server.DataLayer.Models
{
    public class ItemTag : BaseModel
    {
        public string Name { get; set; }
        public virtual IList<CollectionItem> Items { get; set; }
    }
}