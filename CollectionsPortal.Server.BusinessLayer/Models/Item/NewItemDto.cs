namespace CollectionsPortal.Server.BusinessLayer.Models.Item
{
    public class NewItemDto
    {
        public string Name { get; set; }
        public IList<ItemTagDto> Tags { get; set; }
        public string? CustomString1Value { get; set; }
        public string? CustomString2Value { get; set; }
        public string? CustomString3Value { get; set; }
        public string? CustomInt1Value { get; set; }
        public string? CustomInt2Value { get; set; }
        public string? CustomInt3Value { get; set; }
        public string? CustomText1Value { get; set; }
        public string? CustomText2Value { get; set; }
        public string? CustomText3Value { get; set; }
        public string? CustomBool1Value { get; set; }
        public string? CustomBool2Value { get; set; }
        public string? CustomBool3Value { get; set; }
        public string? CustomDate1Value { get; set; }
        public string? CustomDate2Value { get; set; }
        public string? CustomDate3Value { get; set; }
    }
}
