using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerializeTable;

public class ItemStore : MonoBehaviour
{
    /// <summary>PlayerItemManagement</summary>
    PlayerItemManagement _pim;
    [Tooltip("Playerが範囲内にいるときに表示する案内用のUI")]
    [SerializeField] private GameObject _infomationUI = null;
    [Tooltip("アイテムストアで売っているアイテム (売っているアイテム, 必要なもの<必要なアイテム名, 個数>)")]
    [SerializeField] Dictionary<string, Dictionary<string, int>> _sellItemDic;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerItemManagement>(out PlayerItemManagement player))
        {
            _pim = player;
        }
    }

}
//public class DicTable : DicTableBase(string, int)
