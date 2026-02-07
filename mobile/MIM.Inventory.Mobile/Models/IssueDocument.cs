namespace MIM.Inventory.Mobile.Models
{
    public class IssueDocument
    {
        public IssueDocument(IssuingOrder header, IReadOnlyList<IssuingDetail> lines)
        {
            Header = header;
            Lines = lines;
        }

        public IssuingOrder Header { get; }
        public IReadOnlyList<IssuingDetail> Lines { get; }
    }
}
