using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.Utilities
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
