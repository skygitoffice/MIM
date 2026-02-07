using System.Collections.ObjectModel;
using System.Windows.Input;
using MIM.Inventory.Mobile.Services;

namespace MIM.Inventory.Mobile.ViewModels
{
    public class VoidViewModel : ViewModelBase
    {
        private readonly IVoidService _voidService;
        private string _voidNo = string.Empty;
        private string _reason = string.Empty;
        private string _statusMessage = "";

        public VoidViewModel(IVoidService voidService)
        {
            _voidService = voidService;
            Items = new ObservableCollection<string> { "註銷項目: G7007", "註銷項目: H8008" };
            SubmitCommand = new Command(async () => await SubmitAsync());
        }

        public string VoidNo
        {
            get => _voidNo;
            set => SetProperty(ref _voidNo, value);
        }

        public string Reason
        {
            get => _reason;
            set => SetProperty(ref _reason, value);
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
            await _voidService.SubmitAsync(VoidNo, Reason);
            StatusMessage = "已送出註銷作業 (placeholder)。";
        }
    }
}
