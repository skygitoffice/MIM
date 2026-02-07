using MIM.Inventory.Mobile.ViewModels;

namespace MIM.Inventory.Mobile.Pages
{
    public partial class IssuePage : ContentPage
    {
        public IssuePage(IssueViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is IssueViewModel viewModel)
            {
                await viewModel.InitializeAsync();
            }
        }
    }
}
