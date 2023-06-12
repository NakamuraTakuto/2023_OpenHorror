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
    [Tooltip("アイテムストアで売っているアイテム")]
    [SerializeField] List<GameObject> _sellItem = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerItemManagement>(out PlayerItemManagement player))
        {
            _infomationUI.SetActive(true);
            _pim = player;
        }
    }
    //ItemStoreが開かれた時に実行する
    //_sellItem.Count回ループを回しての条件を調べる
    private void Update()
    {
        
    }
}