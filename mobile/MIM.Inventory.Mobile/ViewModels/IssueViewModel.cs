using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using MIM.Inventory.Mobile.Models;
using MIM.Inventory.Mobile.Services;

namespace MIM.Inventory.Mobile.ViewModels
{
    public class IssueViewModel : ViewModelBase
    {
        private readonly IIssueService _issueService;
        private string _workOrder = string.Empty;
        private string _materialOrRfid = string.Empty;
        private string _quantity = string.Empty;
        private string _remark = string.Empty;
        private string _statusMessage = string.Empty;
        private Color _statusColor = Colors.Gray;
        private string _dbStatusText = "DB: Unknown";
        private Color _dbStatusColor = Colors.Gray;
        private bool _isInitialized;

        public IssueViewModel(IIssueService issueService)
        {
            _issueService = issueService;
            RecentIssues = new ObservableCollection<IssueSummaryItem>();
            SubmitCommand = new Command(async () => await SubmitAsync());
            RefreshCommand = new Command(async () => await RefreshAsync());
            SelectIssueCommand = new Command<SelectionChangedEventArgs>(async args => await OpenDetailAsync(args?.CurrentSelection?.FirstOrDefault() as IssueSummaryItem));
        }

        public string WorkOrder
        {
            get => _workOrder;
            set => SetProperty(ref _workOrder, value);
        }

        public string MaterialOrRfid
        {
            get => _materialOrRfid;
            set => SetProperty(ref _materialOrRfid, value);
        }

        public string Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }

        public string Remark
        {
            get => _remark;
            set => SetProperty(ref _remark, value);
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public Color StatusColor
        {
            get => _statusColor;
            set => SetProperty(ref _statusColor, value);
        }

        public string DbStatusText
        {
            get => _dbStatusText;
            set => SetProperty(ref _dbStatusText, value);
        }

        public Color DbStatusColor
        {
            get => _dbStatusColor;
            set => SetProperty(ref _dbStatusColor, value);
        }

        public ObservableCollection<IssueSummaryItem> RecentIssues { get; }

        public ICommand SubmitCommand { get; }

        public ICommand RefreshCommand { get; }

        public ICommand SelectIssueCommand { get; }

        public async Task InitializeAsync()
        {
            if (_isInitialized)
            {
                await RefreshAsync();
                return;
            }

            _isInitialized = true;
            await RefreshAsync();
        }

        private async Task RefreshAsync()
        {
            await UpdateDbStatusAsync();

            try
            {
                var issues = await _issueService.GetRecentIssuesAsync(20);
                RecentIssues.Clear();
                foreach (var issue in issues)
                {
                    RecentIssues.Add(new IssueSummaryItem
                    {
                        Id = issue.Id,
                        DiffVoucherno = issue.DiffVoucherno,
                        IssuingDate = issue.IssuingDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                        MachineNo = issue.MachineNo,
                        IssuedBy = issue.IssuedBy,
                        StatusText = issue.IsValid ? "有效" : "已註銷",
                        StatusColor = issue.IsValid ? Colors.Green : Colors.OrangeRed
                    });
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"查詢失敗: {ex.Message}";
                StatusColor = Colors.OrangeRed;
            }
        }

        private async Task SubmitAsync()
        {
            if (string.IsNullOrWhiteSpace(WorkOrder) || string.IsNullOrWhiteSpace(MaterialOrRfid) || string.IsNullOrWhiteSpace(Quantity))
            {
                StatusMessage = "請輸入工單、料號或RFID，以及數量。";
                StatusColor = Colors.OrangeRed;
                return;
            }

            if (!decimal.TryParse(Quantity, NumberStyles.Number, CultureInfo.InvariantCulture, out var issuedWeight))
            {
                StatusMessage = "數量格式不正確。";
                StatusColor = Colors.OrangeRed;
                return;
            }

            int? materialId = null;
            if (int.TryParse(MaterialOrRfid, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedId))
            {
                materialId = parsedId;
            }

            var request = new IssueCreateRequest
            {
                WarehouseId = 1,
                IssuingDate = DateTime.Today,
                MachineNo = WorkOrder,
                Destination = "機台",
                IssuedBy = "mobile",
                Remark = Remark,
                MaterialOrRfid = MaterialOrRfid,
                MaterialId = materialId,
                IssuedWeight = issuedWeight,
                Unit = "G"
            };

            try
            {
                var document = await _issueService.CreateIssueAsync(request);
                StatusMessage = $"領料單 {document.Header.DiffVoucherno} 已建立。";
                StatusColor = Colors.Green;
                await RefreshAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = $"提交失敗: {ex.Message}";
                StatusColor = Colors.OrangeRed;
            }
        }

        private async Task OpenDetailAsync(IssueSummaryItem? item)
        {
            if (item is null)
            {
                return;
            }

            await Shell.Current.GoToAsync($"{nameof(Pages.IssueDetailPage)}?IssueId={item.Id}");
        }

        private async Task UpdateDbStatusAsync()
        {
            try
            {
                var connected = await _issueService.CheckConnectionAsync();
                DbStatusText = connected ? "DB: Connected" : "DB: Disconnected";
                DbStatusColor = connected ? Colors.Green : Colors.OrangeRed;
            }
            catch (Exception ex)
            {
                DbStatusText = $"DB: Error ({ex.Message})";
                DbStatusColor = Colors.OrangeRed;
            }
        }
    }
}
