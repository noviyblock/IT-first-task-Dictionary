using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicMvvmSample.Models
{
    public class Dictionary<TKey, TValue>
    {
        private readonly List<KeyValuePair<TKey, TValue>> _items = new List<KeyValuePair<TKey, TValue>>();

        public int Count => _items.Count;
        public bool IsEmpty => _items.Count == 0;
        public TKey[] Keys => _items.Select(pair => pair.Key!).ToArray();
        public TValue[] Values => _items.Select(pair => pair.Value!).ToArray();
        public KeyValuePair<TKey, TValue>[] KeyValuePairs => _items.ToArray();

        public void Add(TKey key, TValue value)
        {
            if (key is null) 
                throw new ArgumentNullException(nameof(key), "Key cannot be null.");

            if (_items.Any(pair => pair.Key is not null && EqualityComparer<TKey>.Default.Equals(pair.Key, key)))
                throw new ArgumentException("Key already exists.");

            _items.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public bool Remove(TKey key)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key), "Key cannot be null.");

            int index = _items.FindIndex(pair => pair.Key is not null && EqualityComparer<TKey>.Default.Equals(pair.Key, key));

            if (index == -1)
                return false;

            _items.RemoveAt(index);
            return true;
        }

        public bool ContainsKey(TKey key)
        {
            if (key is null)
                return false;

            return _items.Any(pair => pair.Key is not null && EqualityComparer<TKey>.Default.Equals(pair.Key, key));
        }

        public void Clear() => _items.Clear();

        public TValue this[TKey key]
        {
            get
            {
                if (key is null)
                    throw new ArgumentNullException(nameof(key), "Key cannot be null.");

                var pair = _items.FirstOrDefault(pair => pair.Key is not null && EqualityComparer<TKey>.Default.Equals(pair.Key, key));

                if (pair.Key is null)
                    throw new KeyNotFoundException("Key not found.");

                return pair.Value!;
            }
            set
            {
                if (key is null)
                    throw new ArgumentNullException(nameof(key), "Key cannot be null.");

                for (int i = 0; i < _items.Count; i++)
                {
                    if (_items[i].Key is not null && EqualityComparer<TKey>.Default.Equals(_items[i].Key, key))
                    {
                        _items[i] = new KeyValuePair<TKey, TValue>(key, value);
                        return;
                    }
                }

                throw new KeyNotFoundException("Key not found.");
            }
        }
        
        public bool TryFind(TKey key, out TValue value)
        {
            var item = _items.FirstOrDefault(pair => EqualityComparer<TKey>.Default.Equals(pair.Key, key));
            if (EqualityComparer<KeyValuePair<TKey, TValue>>.Default.Equals(item, default(KeyValuePair<TKey, TValue>)))
            {
                value = default!;
                return false;
            }
            value = item.Value;
            return true;
        }
    }
}
