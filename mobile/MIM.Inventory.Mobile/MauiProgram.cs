using Microsoft.Extensions.Logging;
using MIM.Inventory.Mobile.Pages;
using MIM.Inventory.Mobile.Services;
using MIM.Inventory.Mobile.ViewModels;

namespace MIM.Inventory.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton(new ApiService(new HttpClient
            {
                BaseAddress = new Uri(AppConstants.ApiBaseAddress)
            }));
            builder.Services.AddSingleton<ITransferService, TransferService>();
            builder.Services.AddSingleton<IIssueService, IssueService>();
            builder.Services.AddSingleton<IStocktakeService, StocktakeService>();
            builder.Services.AddSingleton<IVoidService, VoidService>();

            builder.Services.AddTransient<TransferViewModel>();
            builder.Services.AddTransient<IssueViewModel>();
            builder.Services.AddTransient<StocktakeViewModel>();
            builder.Services.AddTransient<VoidViewModel>();

            builder.Services.AddTransient<TransferPage>();
            builder.Services.AddTransient<IssuePage>();
            builder.Services.AddTransient<StocktakePage>();
            builder.Services.AddTransient<VoidPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
