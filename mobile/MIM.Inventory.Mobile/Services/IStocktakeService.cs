namespace MIM.Inventory.Mobile.Services
{
    public interface IStocktakeService
    {
        Task SubmitAsync(string stocktakeNo, string warehouse, CancellationToken cancellationToken = default);
    }
}
