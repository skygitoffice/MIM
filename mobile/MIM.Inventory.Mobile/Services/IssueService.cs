namespace MIM.Inventory.Mobile.Services
{
    public class IssueService : IIssueService
    {
        public IssueService(ApiService apiService)
        {
            ApiService = apiService;
        }

        public ApiService ApiService { get; }

        public Task SubmitAsync(string issueNo, string requester, string costCenter, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
