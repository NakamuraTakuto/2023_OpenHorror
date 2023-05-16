using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class PlayerItemManagement : MonoBehaviour
{
    [Header("PlayerのItemインベントリ")]
    [SerializeField] GameObject _itemCanvas;
    [Header("ItemButtonPreFab")]
    [SerializeField] GameObject _itemButton;
    [Header("Itemが取得可能状態にあるときに表示されるpanel")]
    [SerializeField] GameObject _itemPanel;
    [SerializeField] List<GameObject> _itemList = new();
    bool _trrigerPrime = false;

    private void Start()
    {
        if (_itemCanvas == null || _itemButton == null || _itemPanel == null)
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
            _itemPanel.SetActive(true);
            //ItemBoxの子オブジェクトとしてButtonを生成する
            var InstantiateObj = Instantiate(_itemButton, _itemCanvas.transform);
            //生成したButtonのOnClickにItemBaseの処理を追加している
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

        if(_itemPanel.activeSelf)
        {
            _itemPanel.SetActive(false);
        }
    }
}
