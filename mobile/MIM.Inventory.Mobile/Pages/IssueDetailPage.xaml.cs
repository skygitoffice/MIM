using MIM.Inventory.Mobile.ViewModels;

namespace MIM.Inventory.Mobile.Pages
{
    public partial class IssueDetailPage : ContentPage, IQueryAttributable
    {
        public IssueDetailPage(IssueDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (BindingContext is IssueDetailViewModel viewModel
                && query.TryGetValue("IssueId", out var rawValue)
                && rawValue is string stringValue
                && long.TryParse(stringValue, out var issueId))
            {
                await viewModel.LoadAsync(issueId);
            }
        }
    }
}
