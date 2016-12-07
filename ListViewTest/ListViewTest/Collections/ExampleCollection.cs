using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ListViewTest.Annotations;

namespace ListViewTest.Collections
{
    public class ExampleCollection<T> : IReadOnlyCollection<T>, INotifyPropertyChanged, INotifyCollectionChanged
        where T : INotifyPropertyChanged
    {
        private readonly object _lockObject = new object();

        private IList<T> _items;

        public ExampleCollection(ObservableCollection<T> source)
        {
            Source = source;
            Source.CollectionChanged += SourceOnCollectionChanged;
            _items = new List<T>();
            Refresh();
        }

        public ObservableCollection<T> Source { get; }

        public int Count {
            get
            {
                lock (_lockObject)
                {
                    return _items.Count;
                }
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Refresh()
        {
            lock (_lockObject)
            {
               _items = Source.ToList();
            }
            
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        private void SourceOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var itemsToAdd = e.NewItems.Cast<T>().ToList();
                    Add(e.NewStartingIndex, itemsToAdd.ToList());
                    break;

                case NotifyCollectionChangedAction.Remove:
                    var itemsToRemove = e.OldItems.Cast<T>().ToList();
                    Remove(itemsToRemove);
                    break;

                default:
                    Refresh();
                    break;
            }
        }

        private void Add(int startingIndex, List<T> items)
        {
            if (items.Count == 0)
            {
                return;
            }
            var i = startingIndex;
            lock (_lockObject)
            {
                foreach (var item in items)
                {
                    _items.Insert(i++, item);
                }
            }
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items,
                startingIndex));
        }

        private void Remove(List<T> items)
        {
            if (items.Count == 0)
            {
                return;
            }
            lock (_lockObject)
            {
                foreach (var item in items)
                {
                    _items.Remove(item);
                }
            }
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, items));
        }
    }
}
