namespace RentalDBModels.Views.DBViews
{
    public class NotPopularInventoryGroupByCategories
    {
        public int IdCategory { get; set; }
        public bool IsTypeCategory { get; set; }
        public string InventoryName { get; set; }
        public int IdInventory { get; set; }
        public int UsageCount { get; set; }
    }
}
