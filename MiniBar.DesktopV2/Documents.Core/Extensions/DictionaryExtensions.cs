using System.Collections.Generic;

namespace Documents.Extensions
{
    public static class DictionaryExtensions
    {
        public static void SetDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dict, IDictionary<TKey, TValue> from)
        {
            foreach (var item in from)
            {
                dict.Add(item.Key, item.Value);
            }
        }
    }
}
