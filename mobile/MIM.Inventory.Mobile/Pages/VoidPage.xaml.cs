using MIM.Inventory.Mobile.ViewModels;

namespace MIM.Inventory.Mobile.Pages
{
    public partial class VoidPage : ContentPage
    {
        public VoidPage(VoidViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
