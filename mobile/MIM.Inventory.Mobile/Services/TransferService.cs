namespace MIM.Inventory.Mobile.Services
{
    public class TransferService : ITransferService
    {
        public TransferService(ApiService apiService)
        {
            ApiService = apiService;
        }

        public ApiService ApiService { get; }

        public Task SubmitAsync(string transferNo, string fromLocation, string toLocation, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
