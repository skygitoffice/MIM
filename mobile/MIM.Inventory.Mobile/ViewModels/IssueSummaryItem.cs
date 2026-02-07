using Microsoft.Maui.Graphics;

namespace MIM.Inventory.Mobile.ViewModels
{
    public class IssueSummaryItem
    {
        public long Id { get; init; }
        public string DiffVoucherno { get; init; } = string.Empty;
        public string IssuingDate { get; init; } = string.Empty;
        public string MachineNo { get; init; } = string.Empty;
        public string? IssuedBy { get; init; }
        public string StatusText { get; init; } = string.Empty;
        public Color StatusColor { get; init; } = Colors.Gray;
    }
}
