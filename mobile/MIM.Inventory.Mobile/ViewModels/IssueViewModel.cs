using System.Collections.ObjectModel;
using System.Windows.Input;
using MIM.Inventory.Mobile.Services;

namespace MIM.Inventory.Mobile.ViewModels
{
    public class IssueViewModel : ViewModelBase
    {
        private readonly IIssueService _issueService;
        private string _issueNo = string.Empty;
        private string _requester = string.Empty;
        private string _costCenter = string.Empty;
        private string _statusMessage = "";

        public IssueViewModel(IIssueService issueService)
        {
            _issueService = issueService;
            Items = new ObservableCollection<string> { "料號: C3003", "料號: D4004" };
            SubmitCommand = new Command(async () => await SubmitAsync());
        }

        public string IssueNo
        {
            get => _issueNo;
            set => SetProperty(ref _issueNo, value);
        }

        public string Requester
        {
            get => _requester;
            set => SetProperty(ref _requester, value);
        }

        public string CostCenter
        {
            get => _costCenter;
            set => SetProperty(ref _costCenter, value);
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
            await _issueService.SubmitAsync(IssueNo, Requester, CostCenter);
            StatusMessage = "已送出領料作業 (placeholder)。";
        }
    }
}
