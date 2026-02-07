using MIM.Inventory.Mobile.ViewModels;

namespace MIM.Inventory.Mobile.Pages
{
    public partial class StocktakePage : ContentPage
    {
        public StocktakePage(StocktakeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
