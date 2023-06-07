using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class PlayerItemManagement : MonoBehaviour
{
    /// <summary>�A�C�e���C���x���g����UI</summary>
    [Header("Player��Item�C���x���g��")]
    [SerializeField] GameObject _itemBoxCanvas;
    [Header("ItemButtonPreFab")]
    [SerializeField] GameObject _itemButton;
    /// <summary>Item���擾�\�̎��ɕ\������UI</summary>
    [Header("Item���擾�\��Ԃɂ���Ƃ��ɕ\�������panel")]
    [SerializeField] GameObject _itemPanel;
    [SerializeField] List<GameObject> _itemList = new();
    bool _trrigerPrime = false;
    ItemBase _hitItem;

    private void Start()
    {
        if (_itemBoxCanvas == null || _itemButton == null || _itemPanel == null)
        {
            Debug.Log("�A�^�b�`����Ă��Ȃ��ӏ�������܂�");
        }
    }

    //void ClickProcess()//���N���b�N���̏���
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    if (Physics.Raycast(ray, out RaycastHit hit))
    //    {
    //        //Ray���΂��đΏۂ�ItemBase���p�����Ă����ꍇ�Ɏ��s
    //        if (hit.collider.gameObject.TryGetComponent<ItemBase>(out ItemBase itemBase))
    //        {
    //            //ItemBox�̎q�I�u�W�F�N�g�Ƃ���Button�𐶐�����
    //            var InstantiateObj = Instantiate(_itemButton, _itemCanvas.transform);
    //            //��������Button��OnClick��ItemBase�̏�����ǉ����Ă���
    //            InstantiateObj.GetComponent<Button>().onClick.AddListener(() => itemBase.Action());
    //            InstantiateObj.GetComponentInChildren<Text>().text = itemBase.GetItemName;
    //            _itemList.Add(InstantiateObj);
    //            itemBase.ItemOFF();
    //        }
    //    }
    //}

    void KeyProcess(ItemBase _hitObject)
    {
        if (_hitObject != null && _trrigerPrime)
        {
            //ItemBox�̎q�I�u�W�F�N�g�Ƃ���Button�𐶐�����
            var InstantiateObj = Instantiate(_itemButton, _itemBoxCanvas.transform);

            //��������Button��OnClick��ItemBase�̏�����ǉ����Ă���
            InstantiateObj.GetComponent<Button>().onClick.AddListener(() => _hitObject.Action());
            InstantiateObj.GetComponentInChildren<Text>().text = _hitObject.GetItemName;
            _itemList.Add(InstantiateObj);
            _hitObject.ItemOFF();
            _itemPanel.SetActive(false);    
        }
    }

    void ItemBoxChanger()
    {
        if (_itemBoxCanvas.activeSelf)
        {
            if (_hitItem != null)
            {
                _itemPanel.SetActive(true);
            }

            _itemBoxCanvas.SetActive(false);
        }
        else
        {
            if (_itemPanel.activeSelf)
            {
                _itemPanel.SetActive(false); 
            }

            _itemBoxCanvas.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ItemBoxChanger();
        }
        if (_hitItem != null && Input.GetKeyDown(KeyCode.F))
        {
            KeyProcess(_hitItem);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _trrigerPrime = true;
        
        if (other.gameObject.TryGetComponent(out ItemBase item))
        {
            _hitItem = item;
        }


        if (_hitItem != null && !_itemBoxCanvas.activeSelf)
        {
            _itemPanel.SetActive(true);
            _itemPanel.GetComponentInChildren<Text>().text = $"F {_hitItem.GetItemName}";
        }
        //KeyProcess(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        _trrigerPrime = false;
        _hitItem = null;

        if (_itemPanel.activeSelf)
        {
            _itemPanel.SetActive(false);
        }
    }
}
