using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemBase : MonoBehaviour
{
    [Header("Item名を設定")]
    [SerializeField] private string _itemName;
    public string GetItemName => _itemName;

    private void Start()
    {
        if (_itemName == null || GetItemName == null)
        {
            Debug.Log("Item名がNullです");
        }
    }

    public abstract void Action(); //インベントリで選択された時の処理
}
