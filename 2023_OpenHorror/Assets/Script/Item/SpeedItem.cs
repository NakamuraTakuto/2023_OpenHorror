using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : ItemBase
{
    [SerializeField] float _speedUpValue = 2;
    public override void Action()
    {
        GameObject.Find("Player").GetComponent<PlayerMove>().MoveSpeed += _speedUpValue;
    }
}
