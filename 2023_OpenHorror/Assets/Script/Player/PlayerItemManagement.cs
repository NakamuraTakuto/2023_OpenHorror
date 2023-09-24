using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class PlayerItemManagement : MonoBehaviour
{
    [Tooltip("�A�C�e���C���x���g�����܂Ƃ߂���Obj")]
    [SerializeField] private GameObject _itemBoxCanvas;
    [Tooltip("���ۂ�Button��ǉ�����C���x���g��UI")]
    [SerializeField] private GameObject _itemBox;
    [Tooltip("ItemButtonPreFab, �C���x���g���ɂ���Button")]
    [SerializeField] private GameObject _itemButton;
    [Tooltip("Item���擾�\��Ԃɂ���Ƃ��ɕ\�������panel")]
    [SerializeField] private GameObject _itemPanel;
    [Tooltip("Player���������Ă���A�C�e��")]
    [SerializeField] public List<string> PlayerItemList = new();
    [Tooltip("Player���������Ă�����z")]
    [SerializeField] public int PlayerMoney = 0;
    private bool _trrigerPrime = false;
    private ItemBase _hitItem;
    private List<GameObject> buttonList = new();

    private void Start()
    {
        if (_itemBoxCanvas == null || _itemButton == null || _itemPanel == null || _itemBox == null)
        {
            Debug.Log("�A�^�b�`����Ă��Ȃ��ӏ�������܂�");
        }
    }
    public void KeyProcess(ItemBase _hitObject)
    {
        if (_hitObject != null && _trrigerPrime)
        {
            //ItemBox�̎q�I�u�W�F�N�g�Ƃ���Button�𐶐�����
            var InstantiateObj = Instantiate(_itemButton, _itemBox.transform);

            //��������Button��OnClick��ItemBase�̏�����ǉ����Ă���
            InstantiateObj.GetComponent<Button>().onClick.AddListener(() => _hitObject.Action());
            InstantiateObj.GetComponentInChildren<Text>().text = _hitObject.GetItemName.ToString();
            buttonList.Add(InstantiateObj);
            PlayerItemList.Add(_hitObject.GetItemName.ToString());
            _hitObject.ItemOFF();
            _itemPanel.SetActive(false);    
        }
    }

    public void ButtonRemove(int x)
    {
        Destroy(buttonList[x]);
        buttonList.RemoveAt(x);
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