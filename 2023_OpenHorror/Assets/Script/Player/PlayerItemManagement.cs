using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class PlayerItemManagement : MonoBehaviour
{
    /// <summary>アイテムインベントリのUI</summary>
    [Header("PlayerのItemインベントリ")]
    [SerializeField] GameObject _itemBoxCanvas;
    [Header("ItemButtonPreFab")]
    [SerializeField] GameObject _itemButton;
    /// <summary>Itemが取得可能の時に表示するUI</summary>
    [Header("Itemが取得可能状態にあるときに表示されるpanel")]
    [SerializeField] GameObject _itemPanel;
    [SerializeField] List<GameObject> _itemList = new();
    bool _trrigerPrime = false;
    GameObject _hitItem;

    private void Start()
    {
        if (_itemBoxCanvas == null || _itemButton == null || _itemPanel == null)
        {
            Debug.Log("アタッチされていない箇所があります");
        }
    }

    //void ClickProcess()//左クリック時の処理
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    if (Physics.Raycast(ray, out RaycastHit hit))
    //    {
    //        //Rayを飛ばして対象がItemBaseを継承していた場合に実行
    //        if (hit.collider.gameObject.TryGetComponent<ItemBase>(out ItemBase itemBase))
    //        {
    //            //ItemBoxの子オブジェクトとしてButtonを生成する
    //            var InstantiateObj = Instantiate(_itemButton, _itemCanvas.transform);
    //            //生成したButtonのOnClickにItemBaseの処理を追加している
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
            //_itemPanel.GetComponent<Text>().text = $"F {itemBase.GetItemName}";
            //ItemBoxの子オブジェクトとしてButtonを生成する
            var InstantiateObj = Instantiate(_itemButton, _itemBoxCanvas.transform);
            //生成したButtonのOnClickにItemBaseの処理を追加している
            InstantiateObj.GetComponent<Button>().onClick.AddListener(() => itemBase.Action());
            InstantiateObj.GetComponentInChildren<Text>().text = itemBase.GetItemName;
            _itemList.Add(InstantiateObj);
            itemBase.ItemOFF();
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
        _hitItem = other.gameObject;

        if (_hitItem.GetComponent<ItemBase>() != null && !_itemBoxCanvas.activeSelf)
        {
            _itemPanel.SetActive(true);
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
