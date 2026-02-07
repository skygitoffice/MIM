using System.Collections.ObjectModel;
using System.Windows.Input;
using MIM.Inventory.Mobile.Services;

namespace MIM.Inventory.Mobile.ViewModels
{
    public class TransferViewModel : ViewModelBase
    {
        private readonly ITransferService _transferService;
        private string _transferNo = string.Empty;
        private string _fromLocation = string.Empty;
        private string _toLocation = string.Empty;
        private string _statusMessage = "";

        public TransferViewModel(ITransferService transferService)
        {
            _transferService = transferService;
            Items = new ObservableCollection<string> { "料號: A1001", "料號: B2002" };
            SubmitCommand = new Command(async () => await SubmitAsync());
        }

        public string TransferNo
        {
            get => _transferNo;
            set => SetProperty(ref _transferNo, value);
        }

        public string FromLocation
        {
            get => _fromLocation;
            set => SetProperty(ref _fromLocation, value);
        }

        public string ToLocation
        {
            get => _toLocation;
            set => SetProperty(ref _toLocation, value);
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
            await _transferService.SubmitAsync(TransferNo, FromLocation, ToLocation);
            StatusMessage = "已送出調撥作業 (placeholder)。";
        }
    }
}
