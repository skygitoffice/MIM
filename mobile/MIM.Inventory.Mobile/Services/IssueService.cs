using MIM.Inventory.Mobile.Models;
using MIM.Inventory.Mobile.Repositories;

namespace MIM.Inventory.Mobile.Services
{
    public class IssueService : IIssueService
    {
        private readonly IIssuingRepository _issuingRepository;

        public IssueService(IIssuingRepository issuingRepository)
        {
            _issuingRepository = issuingRepository;
        }

        public Task<IssueDocument> CreateIssueAsync(IssueCreateRequest request, CancellationToken cancellationToken = default)
        {
            return _issuingRepository.CreateIssueAsync(request, cancellationToken);
        }

        public Task<IReadOnlyList<IssueSummary>> GetRecentIssuesAsync(int limit, CancellationToken cancellationToken = default)
        {
            return _issuingRepository.GetRecentIssuesAsync(limit, cancellationToken);
        }

        public Task<IssueDocument?> GetIssueDetailAsync(long id, CancellationToken cancellationToken = default)
        {
            return _issuingRepository.GetIssueDetailAsync(id, cancellationToken);
        }

        public Task<bool> VoidIssueAsync(long id, string updatedBy, CancellationToken cancellationToken = default)
        {
            return _issuingRepository.VoidIssueAsync(id, updatedBy, cancellationToken);
        }

        public Task<bool> CheckConnectionAsync(CancellationToken cancellationToken = default)
        {
            return _issuingRepository.CanConnectAsync(cancellationToken);
        }
    }
}
