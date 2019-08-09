using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Extensions
{
    public static class DictionaryExtensions
    {
        public static void SetDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dict, IDictionary<TKey, TValue> from)
        {
            foreach(var item in from)
            {
                dict.Add(item.Key, item.Value);
            }
        }
    }
}
