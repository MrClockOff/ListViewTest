# ListViewTest
Demonstrates the `ListView` issue on `CollectionChanged` event on Windows 8.1

This example contains `ExampleCollection<T>` which implements `IReadOnlyCollection`, `INotifyPropertyChanged` and `INotifyCollectionChanged` interfaces.

The source of collection is `ObservableCollection<T>`. Changes to source will trigger `ExampleCollection` `CollectionChanged` event. 

When `ExampleCollection` invokes `CollectionChanged` event with event argument `Action == NotifyCollectionChangedAction.Reset` ListView is not reseting.

NOTE: iOS and Android handle this case wihout any issues. 