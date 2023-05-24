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

    void KeyProcess(GameObject _hitObject)
    {
        if (_hitObject.TryGetComponent<ItemBase>(out ItemBase itemBase) && _trrigerPrime)
        {
            _itemPanel.GetComponent<Text>().text = $"F {itemBase.GetItemName}";
            _itemPanel.SetActive(true);
            //ItemBox�̎q�I�u�W�F�N�g�Ƃ���Button�𐶐�����
            var InstantiateObj = Instantiate(_itemButton, _itemBoxCanvas.transform);
            //��������Button��OnClick��ItemBase�̏�����ǉ����Ă���
            InstantiateObj.GetComponent<Button>().onClick.AddListener(() => itemBase.Action());
            InstantiateObj.GetComponentInChildren<Text>().text = itemBase.GetItemName;
            _itemList.Add(InstantiateObj);
            itemBase.ItemOFF();
        }
    }

    void ItemBoxChanger()
    {
        if (_itemBoxCanvas.activeSelf)
        {
            _itemBoxCanvas.SetActive(false);
        }
        else
        {
            _itemBoxCanvas.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ItemBoxChanger();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _trrigerPrime = true;

        if (!_itemBoxCanvas.activeSelf)
        {
            KeyProcess(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _trrigerPrime = false;

        if(_itemPanel.activeSelf)
        {
            _itemPanel.SetActive(false);
        }
    }
}
