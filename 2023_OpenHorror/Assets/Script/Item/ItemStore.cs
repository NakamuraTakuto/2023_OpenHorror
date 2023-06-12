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
    [Tooltip("�A�C�e���X�g�A�Ŕ����Ă���A�C�e��")]
    [SerializeField] List<GameObject> _sellItem = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerItemManagement>(out PlayerItemManagement player))
        {
            _infomationUI.SetActive(true);
            _pim = player;
        }
    }
    //ItemStore���J���ꂽ���Ɏ��s����
    //_sellItem.Count�񃋁[�v���񂵂Ă̏����𒲂ׂ�
    private void Update()
    {
        
    }
}