namespace MIM.Inventory.Mobile.Services
{
    public interface ITransferService
    {
        Task SubmitAsync(string transferNo, string fromLocation, string toLocation, CancellationToken cancellationToken = default);
    }
}
