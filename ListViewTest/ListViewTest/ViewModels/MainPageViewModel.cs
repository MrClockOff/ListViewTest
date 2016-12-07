using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ListViewTest.Collections;
using ListViewTest.ValueObjects;

namespace ListViewTest.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ExampleCollection<Item> _collection;

        public ExampleCollection<Item> Collection
        {
            get { return _collection; }
            set
            {
                if (Equals(_collection, value))
                {
                    return;
                }
                _collection = value;
                _collection.CollectionChanged += CollectionOnCollectionChanged;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<int> CollectionChanged;

        public MainPageViewModel(ObservableCollection<Item> collection)
        {
            Task.Run(
                () =>
                {
                    for (var i = 0; i < 50; i++)
                    {
                        collection.Add(new Item(i.ToString()));
                    }
                    Collection = new ExampleCollection<Item>(collection);
                });
        }

        private void CollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            CollectionChanged?.Invoke(this, ((ExampleCollection<Item>) sender).Count);
        }

        [Annotations.NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}