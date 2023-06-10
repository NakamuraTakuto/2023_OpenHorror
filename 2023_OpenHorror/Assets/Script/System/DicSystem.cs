using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerializeTable;

[System.Serializable]
public class DicTable : DicTableBase<string, int, DicPairs> { }

[System.Serializable]
public class DicPairs : KeyAndValue <string, int>
{
    public DicPairs(string key, int value) : base(key, value) { }
}


public class DicSystem : MonoBehaviour
{
}
