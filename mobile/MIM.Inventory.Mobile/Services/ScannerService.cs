namespace MIM.Inventory.Mobile.Services;

public interface IScannerService
{
    event EventHandler<string>? ScanReceived;
    void Start();
    void Stop();
}

public sealed class ScannerService : IScannerService
{
    public event EventHandler<string>? ScanReceived;

    public void Start()
    {
        // Placeholder for keyboard-input-based scanner wiring.
    }

    public void Stop()
    {
        // Placeholder for keyboard-input-based scanner wiring.
    }

    public void RaiseScan(string data)
    {
        ScanReceived?.Invoke(this, data);
    }
}
