namespace MIM.Inventory.Mobile.Services
{
    public class VoidService : IVoidService
    {
        public VoidService(ApiService apiService)
        {
            ApiService = apiService;
        }

        public ApiService ApiService { get; }

        public Task SubmitAsync(string voidNo, string reason, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
