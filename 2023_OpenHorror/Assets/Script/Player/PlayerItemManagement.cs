using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class PlayerItemManagement : MonoBehaviour
{
    [Tooltip("アイテムインベントリをまとめる空のObj")]
    [SerializeField] private GameObject _itemBoxCanvas;
    [Tooltip("実際にButtonを追加するインベントリUI")]
    [SerializeField] private GameObject _itemBox;
    [Tooltip("ItemButtonPreFab, インベントリにだすButton")]
    [SerializeField] private GameObject _itemButton;
    [Tooltip("Itemが取得可能状態にあるときに表示されるpanel")]
    [SerializeField] private GameObject _itemPanel;
    [Tooltip("Playerが所持しているアイテム")]
    [SerializeField] public List<string> PlayerItemList = new();
    [Tooltip("Playerが所持している金額")]
    [SerializeField] public int PlayerMoney = 0;
    private bool _trrigerPrime = false;
    private ItemBase _hitItem;
    private List<GameObject> buttonList = new();

    private void Start()
    {
        if (_itemBoxCanvas == null || _itemButton == null || _itemPanel == null || _itemBox == null)
        {
            Debug.Log("アタッチされていない箇所があります");
        }
    }
    public void KeyProcess(ItemBase _hitObject)
    {
        if (_hitObject != null && _trrigerPrime)
        {
            //ItemBoxの子オブジェクトとしてButtonを生成する
            var InstantiateObj = Instantiate(_itemButton, _itemBox.transform);

            //生成したButtonのOnClickにItemBaseの処理を追加している
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