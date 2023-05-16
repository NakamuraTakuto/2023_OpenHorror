using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class PlayerItemManagement : MonoBehaviour
{
    [Header("Player��Item�C���x���g��")]
    [SerializeField] GameObject _itemCanvas;
    [Header("ItemButtonPreFab")]
    [SerializeField] GameObject _itemButton;
    [SerializeField] List<GameObject> _itemList = new();
    bool _trrigerPrime = false;


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
            //ItemBox�̎q�I�u�W�F�N�g�Ƃ���Button�𐶐�����
            var InstantiateObj = Instantiate(_itemButton, _itemCanvas.transform);
            //��������Button��OnClick��ItemBase�̏�����ǉ����Ă���
            InstantiateObj.GetComponent<Button>().onClick.AddListener(() => itemBase.Action());
            InstantiateObj.GetComponentInChildren<Text>().text = itemBase.GetItemName;
            _itemList.Add(InstantiateObj);
            itemBase.ItemOFF();
        }
    }

    void ItemBoxChanger()
    {
        if (_itemCanvas.activeSelf)
        {
            _itemCanvas.SetActive(false);
        }
        else
        {
            _itemCanvas.SetActive(true);
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

        if (!_itemCanvas.activeSelf)
        {
            KeyProcess(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _trrigerPrime = false;
    }
}
