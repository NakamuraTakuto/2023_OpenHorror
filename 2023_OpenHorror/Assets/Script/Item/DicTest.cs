using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Serialize;


/// <summary>
/// ジェネリックを隠すために継承してしまう
/// [System.Serializable]を書くのを忘れない
/// </summary>
[System.Serializable]
public class SampleTable : TableBase<string,int, SamplePair>
{


}

/// <summary>
/// ジェネリックを隠すために継承してしまう
/// [System.Serializable]を書くのを忘れない
/// </summary>
[System.Serializable]
public class SamplePair : KeyAndVlueSample<string, int>
{

    public SamplePair(string key, int value) : base(key, value)
    {

    }
}

public class DicTest : MonoBehaviour
{

    //Inspectorに表示できるデータテーブル
    [SerializeField] SampleTable sample;

    //void Awake()
    //{
    //    //内容を列挙
    //    foreach (KeyValuePair<string, int> pair in sample.GetTable())
    //    {
    //        Debug.Log("Key : " + pair.Key + "  Value : " + pair.Value);
    //    }
    //}

}

