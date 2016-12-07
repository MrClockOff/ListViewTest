using System.Collections.ObjectModel;
using System.Windows.Input;
using ListViewTest.ValueObjects;
using Xamarin.Forms;

namespace ListViewTest.ViewModels
{
    public class MenuPageViewModel
    {
        private readonly ObservableCollection<Item> _collection;

        public ICommand AddItemCommand
        {
            get
            {
                return new Command(() =>
                {
                    _collection.Add(new Item());
                });
            }
        }

        public ICommand ClearItemsCommand
        {
            get
            {
                return new Command(() =>
                {
                    _collection.Clear();
                });
            }
        }

        public MenuPageViewModel(ObservableCollection<Item> collection)
        {
            _collection = collection;
        }
    }
}