using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemManagement : MonoBehaviour
{
    [Header("Player��Item�C���x���g��")]
    [SerializeField] GameObject _itemCanvas;
    [Header("ItemButtonPreFab")]
    [SerializeField] GameObject _itemButton;
    [SerializeField] List<GameObject> _itemList = new();

    void ClickProcess()//���N���b�N���̏���
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //Ray���΂��đΏۂ�ItemBase���p�����Ă����ꍇ�Ɏ��s
            if (hit.collider.gameObject.TryGetComponent<ItemBase>(out ItemBase itemBase))
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
        if (Input.GetButton("Fire1") && !_itemCanvas.activeSelf)
        {
            ClickProcess();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ItemBoxChanger();
        }

    }
}
