using DevExpress.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MiniBar.EntityViewModels.Global
{
    public class BindableDictionaryItem<T> : ViewModelBase
    {
        private string key;

        public string Key
        {
            get { return key; }
            set { SetValue(ref key, value, nameof(Key)); }
        }

        private T val;
        public T Value
        {
            get { return val; }
            set { SetValue(ref val, value, nameof(Value)); }
        }
    }

    public class BindableDictionary<T> : ObservableCollection<BindableDictionaryItem<T>>, IDictionary<string, T>
    {
        class BindableDictionaryEnumerator : IEnumerator<KeyValuePair<string, T>>
        {
            BindableDictionary<T> BindableDictionary;
            public BindableDictionaryEnumerator(BindableDictionary<T> bindableDictionary)
            {
                BindableDictionary = bindableDictionary;
            }

            int position = -1;

            public KeyValuePair<string, T> Current
            {
                get
                {
                    try
                    {
                        return new KeyValuePair<string, T>(BindableDictionary[position].Key, BindableDictionary[position].Value);
                    }

                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                position++;
                return (position < BindableDictionary.Count);
            }

            public void Reset()
            {
                position = -1;
            }
        }

        public BindableDictionary()
        {

        }

        public BindableDictionary(IDictionary<string, T> dict)
        {
            foreach (var item in dict)
            {
                Add(item.Key, item.Value);
            }
        }

        public T this[string key]
        {
            get
            {
                foreach (var item in this)
                {
                    if (item.Key == key)
                        return item.Value;
                }
                return default(T);
            }
            set
            {
                foreach (var item in this)
                {
                    if (item.Key == key)
                        item.Value = value;
                }
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                List<string> keys = new List<string>();

                foreach (var item in this)
                {
                    keys.Add(item.Key);
                }
                return keys;
            }
        }

        public ICollection<T> Values
        {
            get
            {
                List<T> values = new List<T>();

                foreach (var item in this)
                {
                    values.Add(item.Value);
                }
                return values;

            }
        }

        public bool IsReadOnly => false;

        public void Add(string key, T value)
        {
            this.Add(new BindableDictionaryItem<T> { Key = key, Value = value });
        }

        public void Add(KeyValuePair<string, T> item)
        {
            Add(item.Key, item.Value);
        }

        public bool Contains(KeyValuePair<string, T> item)
        {
            if (ContainsKey(item.Key))
                return this[item.Key].Equals(item.Value);
            return false;
        }

        public bool ContainsKey(string key)
        {
            return Keys.Contains(key);
        }

        public void CopyTo(KeyValuePair<string, T>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, T> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out T value)
        {
            throw new NotImplementedException();
        }

        IEnumerator<KeyValuePair<string, T>> IEnumerable<KeyValuePair<string, T>>.GetEnumerator()
        {
            return new BindableDictionaryEnumerator(this);
        }

        public Dictionary<string, T> ToDictionary()
        {
            Dictionary<string, T> dictionary = new Dictionary<string, T>();
            foreach (var item in this)
                dictionary.Add(item.Key, item.Value);
            return dictionary;
        }
    }
}
