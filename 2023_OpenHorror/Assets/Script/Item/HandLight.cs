using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLight : ItemBase
{
    
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _handLight;
    public override void Action()
    {
        _player.GetComponent<PlayerController>().GetIK_TF = true;
        _handLight.SetActive(true);
        var pim = _player.GetComponent<PlayerItemManagement>();
        int x = pim.PlayerItemList.IndexOf(GetItemName);
        pim.PlayerItemList.RemoveAt(x);
        pim.ButtonRemove(x);
    }
}
