namespace MIM.Inventory.Mobile.Services
{
    public class StocktakeService : IStocktakeService
    {
        public StocktakeService(ApiService apiService)
        {
            ApiService = apiService;
        }

        public ApiService ApiService { get; }

        public Task SubmitAsync(string stocktakeNo, string warehouse, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
