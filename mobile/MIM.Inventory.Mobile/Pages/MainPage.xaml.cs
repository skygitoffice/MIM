using MIM.Inventory.Mobile.ViewModels;

namespace MIM.Inventory.Mobile.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
