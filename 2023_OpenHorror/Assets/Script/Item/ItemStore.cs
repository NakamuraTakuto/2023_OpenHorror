using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerializeTable;

public class ItemStore : MonoBehaviour
{
    /// <summary>PlayerItemManagement</summary>
    PlayerItemManagement _pim;
    [Tooltip("Player���͈͓��ɂ���Ƃ��ɕ\������ē��p��UI")]
    [SerializeField] private GameObject _infomationUI = null;
    [Tooltip("�A�C�e���X�g�A�Ŕ����Ă���A�C�e�� (�����Ă���A�C�e��, �K�v�Ȃ���<�K�v�ȃA�C�e����, ��>)")]
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
