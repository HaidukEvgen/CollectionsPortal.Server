namespace CollectionsPortal.Server.DataLayer.Models
{
    public class CollectionItem : BaseModel
    {
        public string Name { get; set; }
        public virtual IList<ItemTag> Tags { get; set; }
        public virtual Collection Collection { get; set; }
        public string? CustomString1Value { get; set; }
        public string? CustomString2Value { get; set; }
        public string? CustomString3Value { get; set; }
        public int? CustomInt1Value { get; set; }
        public int? CustomInt2Value { get; set; }
        public int? CustomInt3Value { get; set; }
        public string? CustomText1Value { get; set; }
        public string? CustomText2Value { get; set; }
        public string? CustomText3Value { get; set; }
        public bool? CustomBool1Value { get; set; }
        public bool? CustomBool2Value { get; set; }
        public bool? CustomBool3Value { get; set; }
        public DateOnly? CustomDate1Value { get; set; }
        public DateOnly? CustomDate2Value { get; set; }
        public DateOnly? CustomDate3Value { get; set; }
    }
}
