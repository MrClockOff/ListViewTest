using System.Diagnostics;
using ListViewTest.ViewModels;
using Xamarin.Forms;

namespace ListViewTest.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel viewModel)
        {
            BindingContext = viewModel;

            InitializeComponent();

            viewModel.CollectionChanged += ViewModelOnCollectionChanged;
        }

        private void ViewModelOnCollectionChanged(object sender, int count)
        {
            Debug.WriteLine($"Number of items in source: {count}");
            var listViewCount = 0;
            var listViewEnumerator = ItemsList.ItemsSource.GetEnumerator();
            while (listViewEnumerator.MoveNext())
            {
                listViewCount++;
            }
            Debug.WriteLine($"Number of items in ListView: {listViewCount}");
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView) sender).SelectedItem = null;
        }
    }
}
