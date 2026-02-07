namespace MIM.Inventory.Mobile.Models
{
    public class IssueSummary
    {
        public long Id { get; set; }
        public string DiffVoucherno { get; set; } = string.Empty;
        public DateTime IssuingDate { get; set; }
        public string MachineNo { get; set; } = string.Empty;
        public string? IssuedBy { get; set; }
        public bool IsValid { get; set; }
    }
}
