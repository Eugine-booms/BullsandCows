﻿using System.Collections.Generic;

namespace System.Collections.ObjectModel
{
    public static class ObservableCollection

    {
        public static void Add<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static void AddClear<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            collection.Clear();
            foreach (var item in items)
            {
                collection.Add(items);
            }
        }
    }

}
