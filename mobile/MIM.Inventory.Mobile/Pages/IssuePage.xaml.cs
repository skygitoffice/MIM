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
    }
}
