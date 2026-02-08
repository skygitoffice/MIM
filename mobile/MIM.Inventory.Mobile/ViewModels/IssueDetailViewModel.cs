using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using MIM.Inventory.Mobile.Services;

namespace MIM.Inventory.Mobile.ViewModels
{
    public class IssueDetailViewModel : ViewModelBase
    {
        private readonly IIssueService _issueService;
        private long _issueId;
        private string _diffVoucherno = string.Empty;
        private string _issuingDate = string.Empty;
        private string _machineNo = string.Empty;
        private string _issuedBy = string.Empty;
        private string _remark = string.Empty;
        private string _statusText = string.Empty;
        private Color _statusColor = Colors.Gray;
        private string _statusMessage = string.Empty;
        private Color _messageColor = Colors.Gray;
        private bool _canVoid;

        public IssueDetailViewModel(IIssueService issueService)
        {
            _issueService = issueService;
            Lines = new ObservableCollection<IssueDetailLineItem>();
            VoidCommand = new Command(async () => await VoidAsync(), () => CanVoid);
            RefreshCommand = new Command(async () => await LoadAsync(IssueId));
        }

        public long IssueId
        {
            get => _issueId;
            private set => SetProperty(ref _issueId, value);
        }

        public string DiffVoucherno
        {
            get => _diffVoucherno;
            private set => SetProperty(ref _diffVoucherno, value);
        }

        public string IssuingDate
        {
            get => _issuingDate;
            private set => SetProperty(ref _issuingDate, value);
        }

        public string MachineNo
        {
            get => _machineNo;
            private set => SetProperty(ref _machineNo, value);
        }

        public string IssuedBy
        {
            get => _issuedBy;
            private set => SetProperty(ref _issuedBy, value);
        }

        public string Remark
        {
            get => _remark;
            private set => SetProperty(ref _remark, value);
        }

        public string StatusText
        {
            get => _statusText;
            private set => SetProperty(ref _statusText, value);
        }

        public Color StatusColor
        {
            get => _statusColor;
            private set => SetProperty(ref _statusColor, value);
        }

        public string StatusMessage
        {
            get => _statusMessage;
            private set => SetProperty(ref _statusMessage, value);
        }

        public Color MessageColor
        {
            get => _messageColor;
            private set => SetProperty(ref _messageColor, value);
        }

        public bool CanVoid
        {
            get => _canVoid;
            private set
            {
                if (SetProperty(ref _canVoid, value))
                {
                    (VoidCommand as Command)?.ChangeCanExecute();
                }
            }
        }

        public ObservableCollection<IssueDetailLineItem> Lines { get; }

        public ICommand VoidCommand { get; }

        public ICommand RefreshCommand { get; }

        public async Task LoadAsync(long issueId)
        {
            IssueId = issueId;
            try
            {
                var document = await _issueService.GetIssueDetailAsync(issueId);
                if (document is null)
                {
                    StatusMessage = "查無領料單。";
                    MessageColor = Colors.OrangeRed;
                    return;
                }

                var isValid = document.Header.IsValid ?? true;
                DiffVoucherno = document.Header.DiffVoucherno;
                IssuingDate = document.Header.IssuingDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                MachineNo = document.Header.MachineNo;
                IssuedBy = document.Header.IssuedBy ?? "";
                Remark = document.Header.Remark ?? "";
                CanVoid = isValid;
                StatusText = isValid ? "有效" : "已註銷";
                StatusColor = isValid ? Colors.Green : Colors.OrangeRed;

                Lines.Clear();
                foreach (var line in document.Lines)
                {
                    Lines.Add(new IssueDetailLineItem
                    {
                        MaterialOrRfid = line.BucketRfid ?? line.MaterialId?.ToString(CultureInfo.InvariantCulture) ?? string.Empty,
                        IssuedWeight = line.IssuedWeight,
                        Unit = line.Unit
                    });
                }

                StatusMessage = string.Empty;
                MessageColor = Colors.Gray;
            }
            catch (Exception ex)
            {
                StatusMessage = $"查詢失敗: {ex.Message}";
                MessageColor = Colors.OrangeRed;
            }
        }

        private async Task VoidAsync()
        {
            if (!CanVoid)
            {
                return;
            }

            var mainPage = Application.Current?.MainPage;
            if (mainPage is null)
            {
                StatusMessage = "無法顯示確認視窗。";
                MessageColor = Colors.OrangeRed;
                return;
            }

            var confirm = await mainPage.DisplayAlert(
                "註銷確認",
                $"即將註銷領料單 {DiffVoucherno}，此操作無法撤銷。",
                "確認註銷",
                "取消");

            if (!confirm)
            {
                return;
            }

            try
            {
                var success = await _issueService.VoidIssueAsync(IssueId, "mobile");
                StatusMessage = success ? "已註銷。" : "註銷失敗。";
                MessageColor = success ? Colors.Green : Colors.OrangeRed;
                await LoadAsync(IssueId);
            }
            catch (Exception ex)
            {
                StatusMessage = $"註銷失敗: {ex.Message}";
                MessageColor = Colors.OrangeRed;
            }
        }
    }
}
