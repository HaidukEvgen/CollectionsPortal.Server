using CollectionsPortal.Server.BusinessLayer.Models.Item;

namespace CollectionsPortal.Server.BusinessLayer.Models.Collection
{
    public class CollectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
        public IEnumerable<ItemDto> Items { get; set; }
        public string? CustomString1Name { get; set; }
        public string? CustomString2Name { get; set; }
        public string? CustomString3Name { get; set; }
        public string? CustomInt1Name { get; set; }
        public string? CustomInt2Name { get; set; }
        public string? CustomInt3Name { get; set; }
        public string? CustomText1Name { get; set; }
        public string? CustomText2Name { get; set; }
        public string? CustomText3Name { get; set; }
        public string? CustomBool1Name { get; set; }
        public string? CustomBool2Name { get; set; }
        public string? CustomBool3Name { get; set; }
        public string? CustomDate1Name { get; set; }
        public string? CustomDate2Name { get; set; }
        public string? CustomDate3Name { get; set; }
    }
}
