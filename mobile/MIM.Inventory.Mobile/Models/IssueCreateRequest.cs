namespace MIM.Inventory.Mobile.Models
{
    public class IssueCreateRequest
    {
        public long WarehouseId { get; set; }
        public DateTime IssuingDate { get; set; }
        public string MachineNo { get; set; } = string.Empty;
        public string? Destination { get; set; }
        public string? IssuedBy { get; set; }
        public string? Remark { get; set; }
        public string? MaterialOrRfid { get; set; }
        public long? MaterialId { get; set; }
        public long? VendorId { get; set; }
        public decimal IssuedWeight { get; set; }
        public string Unit { get; set; } = "G";
    }
}
