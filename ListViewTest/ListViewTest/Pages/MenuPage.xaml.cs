using ListViewTest.ViewModels;
using Xamarin.Forms;

namespace ListViewTest.Pages
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage(MenuPageViewModel viewModel)
        {
            BindingContext = viewModel;

            InitializeComponent();
        }
    }
}
