using MIM.Inventory.Mobile.Pages;
using MIM.Inventory.Mobile.Services;
using MIM.Inventory.Mobile.ViewModels;

namespace MIM.Inventory.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(_ => { });

        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton(new HttpClient());
	builder.Services.AddSingleton<ApiService>();
        builder.Services.AddSingleton<IScannerService, ScannerService>();

        return builder.Build();
    }
}
