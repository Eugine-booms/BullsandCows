using System.Collections.ObjectModel;

namespace System.Collections.Generic
{
    public static class EnumerableExtention
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable) => new ObservableCollection<T>(enumerable);
        public static ObservableCollection<T> ToObservableCollection<T>(this List<T> list) => new ObservableCollection<T>(list);
    }
}
