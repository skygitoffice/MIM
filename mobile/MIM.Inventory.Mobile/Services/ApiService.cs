namespace MIM.Inventory.Mobile.Services
{
    public class ApiService
    {
        public ApiService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; }
    }
}
