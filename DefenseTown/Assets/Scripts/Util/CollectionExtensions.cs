using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Core
{
    public static class CollectionExtensions
    {
        public static TValue TryGetValue<TKey,TValue>(this Dictionary<TKey,TValue> dic, TKey key) where TValue : class
        {
            TValue result = null;
            dic.TryGetValue(key, out result);
            return result;
        }
    }
}