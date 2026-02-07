namespace MIM.Inventory.Mobile.ViewModels
{
    public class IssueDetailLineItem
    {
        public string MaterialOrRfid { get; init; } = string.Empty;
        public decimal IssuedWeight { get; init; }
        public string Unit { get; init; } = "G";
        public string DisplayText => $"{MaterialOrRfid} - {IssuedWeight}{Unit}";
    }
}
