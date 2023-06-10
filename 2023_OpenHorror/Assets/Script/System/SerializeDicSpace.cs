using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SerializeTable
{
    [System.Serializable]
    public class DicTableBase <TKey, TValue, Type> where Type : KeyAndValue<TKey, TValue>
    {
        [SerializeField] private List<Type> list;
        private Dictionary<TKey, TValue> table;

        public Dictionary<TKey, TValue> GetTable()
        {
            if (table == null)
            {
                table = ConvertListToDictionary(list);
            }
            return table;
        }
        public List<Type> GetList()
        {
            return list;
        }

        static Dictionary<TKey, TValue> ConvertListToDictionary(List<Type> list)
        {
            Dictionary<TKey, TValue> dic = new Dictionary<TKey, TValue>();

            foreach (KeyAndValue<TKey, TValue> pair in list)
            {
                dic.Add(pair.Key, pair.Value);
            }
            return dic;
        }

    }

    [System.Serializable]
    public class KeyAndValue<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;

        public KeyAndValue(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public KeyAndValue(KeyValuePair<TKey, TValue> pair)
        {
            Key = pair.Key;
            Value = pair.Value;
        }
    }
}