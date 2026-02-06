namespace MIM.Inventory.Mobile.Services;

public sealed class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public HttpClient Client => _httpClient;
}
