using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Serialize
{

    /// <summary>
    /// テーブルの管理クラス
    /// </summary>
    [System.Serializable]
    public class TableBase<TKey, TValue, Type> where Type : KeyAndVlueSample<TKey, TValue>
    {
        [SerializeField]
        private List<Type> list;
        private Dictionary<TKey, TValue> table;


        public Dictionary<TKey, TValue> GetTable()
        {
            if (table == null)
            {
                table = ConvertListToDictionary(list);
            }
            return table;
        }

        /// <summary>
        /// Editor Only
        /// </summary>
        public List<Type> GetList()
        {
            return list;
        }

        //public Dictionary<TKey, TValue> Add<TKey, TValue>(TKey key, TValue value)
        //{
        //    List<KeyValuePair<TKey, TValue>> b = new List<new KeyValuePair<TKey, TValue>(key, value)>;
        //    Dictionary<TKey, TValue> dic = new Dictionary<TKey, TValue>();
        //    return Dictionary
        //}
        static Dictionary<TKey, TValue> ConvertListToDictionary(List<Type> list)
        {
            Dictionary<TKey, TValue> dic = new Dictionary<TKey, TValue>();
            foreach (KeyAndVlueSample<TKey, TValue> pair in list)
            {
                dic.Add(pair.Key, pair.Value);
            }
            return dic;
        }
    }

    /// <summary>
    /// シリアル化できる、KeyValuePair
    /// </summary>
    [System.Serializable]
    public class KeyAndVlueSample<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;
        public KeyAndVlueSample(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public KeyAndVlueSample(KeyValuePair<TKey, TValue> pair)
        {
            Key = pair.Key;
            Value = pair.Value;
        }
    }
}
