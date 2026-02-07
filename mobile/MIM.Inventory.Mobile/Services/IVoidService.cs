namespace MIM.Inventory.Mobile.Services
{
    public interface IVoidService
    {
        Task SubmitAsync(string voidNo, string reason, CancellationToken cancellationToken = default);
    }
}
