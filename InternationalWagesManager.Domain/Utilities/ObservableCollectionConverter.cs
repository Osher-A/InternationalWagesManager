using System.Collections.ObjectModel;

namespace InternationalWagesManager.Domain.Utilities
{
    public static class ObservableCollectionConverter
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
        {
            ObservableCollection<T> collection = new();
            foreach (var item in list)
                collection.Add(item);
            return collection;
        }
    }
}
