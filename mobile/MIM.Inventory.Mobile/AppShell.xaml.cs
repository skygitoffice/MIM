using MIM.Inventory.Mobile.Pages;

namespace MIM.Inventory.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(IssueDetailPage), typeof(IssueDetailPage));
        }
    }
}
