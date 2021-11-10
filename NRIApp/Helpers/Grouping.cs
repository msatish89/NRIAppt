using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;

namespace NRIApp.Helpers
{
    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }

        public Grouping(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
    public class ObservableGroupCollection<K, T> : ObservableCollection<T>
    {
        private readonly K _key;

        public ObservableGroupCollection(IGrouping<K, T> group)
            : base(group)
        {
            _key = group.Key;
        }

        public K Key
        {
            get { return _key; }
        }
    }
}
