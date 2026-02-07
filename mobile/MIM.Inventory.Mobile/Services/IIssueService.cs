namespace MIM.Inventory.Mobile.Services
{
    public interface IIssueService
    {
        Task SubmitAsync(string issueNo, string requester, string costCenter, CancellationToken cancellationToken = default);
    }
}
