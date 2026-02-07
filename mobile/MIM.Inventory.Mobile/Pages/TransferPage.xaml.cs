using MIM.Inventory.Mobile.ViewModels;

namespace MIM.Inventory.Mobile.Pages
{
    public partial class TransferPage : ContentPage
    {
        public TransferPage(TransferViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
