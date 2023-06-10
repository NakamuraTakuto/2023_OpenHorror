using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Serialize;


/// <summary>
/// �W�F�l���b�N���B�����߂Ɍp�����Ă��܂�
/// [System.Serializable]�������̂�Y��Ȃ�
/// </summary>
[System.Serializable]
public class SampleTable : TableBase<string,int, SamplePair>
{


}

/// <summary>
/// �W�F�l���b�N���B�����߂Ɍp�����Ă��܂�
/// [System.Serializable]�������̂�Y��Ȃ�
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

    //Inspector�ɕ\���ł���f�[�^�e�[�u��
    [SerializeField] SampleTable sample;

    //void Awake()
    //{
    //    //���e���
    //    foreach (KeyValuePair<string, int> pair in sample.GetTable())
    //    {
    //        Debug.Log("Key : " + pair.Key + "  Value : " + pair.Value);
    //    }
    //}

}

