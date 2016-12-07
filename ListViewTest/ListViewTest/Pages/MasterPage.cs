using System.Collections.ObjectModel;
using ListViewTest.ValueObjects;
using ListViewTest.ViewModels;
using Xamarin.Forms;

namespace ListViewTest.Pages
{
    public class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            var collection = new ObservableCollection<Item>();

            var mainPageViewModel = new MainPageViewModel(collection);

            Master = new MenuPage (new MenuPageViewModel(collection));
            Detail = new MainPage(mainPageViewModel);
        }
    }
}