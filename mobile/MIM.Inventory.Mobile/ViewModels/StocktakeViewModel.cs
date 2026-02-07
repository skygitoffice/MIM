using System.Collections.ObjectModel;
using System.Windows.Input;
using MIM.Inventory.Mobile.Services;

namespace MIM.Inventory.Mobile.ViewModels
{
    public class StocktakeViewModel : ViewModelBase
    {
        private readonly IStocktakeService _stocktakeService;
        private string _stocktakeNo = string.Empty;
        private string _warehouse = string.Empty;
        private string _statusMessage = "";

        public StocktakeViewModel(IStocktakeService stocktakeService)
        {
            _stocktakeService = stocktakeService;
            Items = new ObservableCollection<string> { "盤點項目: E5005", "盤點項目: F6006" };
            SubmitCommand = new Command(async () => await SubmitAsync());
        }

        public string StocktakeNo
        {
            get => _stocktakeNo;
            set => SetProperty(ref _stocktakeNo, value);
        }

        public string Warehouse
        {
            get => _warehouse;
            set => SetProperty(ref _warehouse, value);
        }

        public ObservableCollection<string> Items { get; }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public ICommand SubmitCommand { get; }

        private async Task SubmitAsync()
        {
            await _stocktakeService.SubmitAsync(StocktakeNo, Warehouse);
            StatusMessage = "已送出盤點作業 (placeholder)。";
        }
    }
}
