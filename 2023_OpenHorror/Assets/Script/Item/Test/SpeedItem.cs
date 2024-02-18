using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : ItemBase
{
    [SerializeField] float _speedUpValue = 2;
    public override void Action()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().MoveSpeed += _speedUpValue;
        var pim = FindObjectOfType<PlayerItemManagement>();
        int x = pim.PlayerItemList.IndexOf(GetItemName);
        pim.PlayerItemList.RemoveAt(x);
        pim.ButtonRemove(x);
    }
}
