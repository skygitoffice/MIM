using MIM.Inventory.Mobile.Models;

namespace MIM.Inventory.Mobile.Repositories
{
    public interface IIssuingRepository
    {
        Task<IssueDocument> CreateIssueAsync(IssueCreateRequest request, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<IssueSummary>> GetRecentIssuesAsync(int limit, CancellationToken cancellationToken = default);
        Task<IssueDocument?> GetIssueDetailAsync(long id, CancellationToken cancellationToken = default);
        Task<bool> VoidIssueAsync(long id, string updatedBy, CancellationToken cancellationToken = default);
        Task<bool> CanConnectAsync(CancellationToken cancellationToken = default);
    }
}
