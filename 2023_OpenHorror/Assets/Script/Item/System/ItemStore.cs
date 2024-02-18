using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ItemStore : MonoBehaviour
{
    [Tooltip("PlauerにアタッチされているPlayerItemManagement")]
    [SerializeField] private PlayerItemManagement _playerIM;
    [Tooltip("Playerが範囲内にいるときに表示する案内用のUI")]
    [SerializeField] private GameObject _infomationUI = null;
    [Tooltip("アイテムストアで売っているアイテム")]
    [SerializeField] private List<GameObject> _sellItem = new();
    [Tooltip("アイテムショップが開かれた時にだすPanel(LayoutGroup推奨)")]
    [SerializeField] private GameObject _shopPanel = null;
    [Tooltip("ショップのpanelにインスタンスするButton")]
    [SerializeField] private GameObject _button;
   // [SerializeField] private string _masterData = "https://script.google.com/macros/s/AKfycbybuatODf8U5GJ6v1NlIdGtglnMFFwxRhBFeWI4ywdunqlKUOS_1lu3BTjmVcvWwFqFGA/exec";
    private Dictionary<string, GameObject> _sellDic = new();
    private Dictionary<string, GameObject> _buttonDic = new();
    private TradeType _tradeType = TradeType.money;
    private bool _isListUP = true;

    //private void Awake()
    //{
    //    StartCoroutine("DataLoad");
    //}

    private void Start()
    {
        if (_shopPanel == null || _infomationUI == null)
        {
            Debug.Log("アタッチされていないものがあります");
        }
        //ItemListUp();
    }

    private void OnEnable()
    {
        if (_isListUP) { ItemListUp(); _isListUP = false; }
        CanSellItem();
    }

    private void Update()
    {
        //ItemStoreが開かれた時に実行する
        if (Input.GetKeyDown(KeyCode.F) && _playerIM != null) { PanelControl(); }

        //Plauerがバックを開いたときにoffにする
        if (Input.GetKeyDown(KeyCode.B)){ _infomationUI.SetActive(false); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerItemManagement player))
        {
            //Playerが範囲内にいるときに案内用UIを表示する
            _infomationUI.SetActive(true);

            //取得したスクリプトを設定する
            _playerIM = player;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //案内用ＵＩの表示を止める
        _infomationUI.SetActive(false);

        //取得していたスクリプトを破棄する
        _playerIM = null;
    }

    private void PanelControl()
    {
        if (_shopPanel.activeSelf)
        {
            _infomationUI.SetActive(true);
            _shopPanel.SetActive(false);
        }
        else
        {
            _infomationUI.SetActive(false);
            _shopPanel.SetActive(true);
            CanSellItem();
        }
    }

    /// <summary>販売するアイテムをストアに並べる</summary>
    private void ItemListUp()
    {
        if (_sellItem != null)
        {
            for (int i = 0; i < _sellItem.Count; i++)
            {
                var button = Instantiate(_button, _shopPanel.transform);
                button.GetComponentInChildren<Text>().text = _sellItem[i].GetComponent<ItemBase>().GetItemName.ToString();
                button.GetComponent<Button>().onClick.AddListener(() => Trade(button.GetComponentInChildren<Text>().text));
                _sellDic.Add(_sellItem[i].GetComponent<ItemBase>().GetItemName.ToString(), _sellItem[i]);
                _buttonDic.Add(_sellItem[i].GetComponent<ItemBase>().GetItemName.ToString(), button);
            }
        }
        else { Debug.Log("リストがNullのため並べられえていません"); }
    }

    /// <summary>売っているアイテムが購入可能状態にあるかの判定</summary>
    private void CanSellItem()
    {
        //売っているアイテムの個数回判定ループ
        for (int i = 0; i < _sellItem.Count; i++)
        {
            var sellItemScript = _sellItem[i].GetComponent<ItemBase>();

            //購入不可の状態にあるときにButtonを使用不可にする
            if (sellItemScript.Condition(_playerIM.PlayerItemList,_playerIM.PlayerMoney))
            {
                _buttonDic[sellItemScript.GetItemName.ToString()].GetComponent<Button>().interactable = true;
            }
            else
            {
                _buttonDic[sellItemScript.GetItemName.ToString()].GetComponent<Button>().interactable = false;
            }
        }
    }
    
    /// <summary>購入処理</summary>
    public void Trade(string itemName)
    { 
        var item = _sellDic[itemName].GetComponent<ItemBase>();
        _tradeType = item.GetTradeTyoe;

        switch (_tradeType)
        {
            //金銭で購入するときの処理
            case TradeType.money:
                //Playerの残高から価格文引いてアイテムを付与する
                _playerIM.PlayerMoney -= item.GetNeedMoney;
                //残高から引いた後に購入可能か再度判定する
                CanSellItem();
                _playerIM.KeyProcess(item);
                break;

            //アイテムで交換・合成する際の処理
            case TradeType.item:
                //必要なアイテムをPlayerのインベントリから抜き取る
                for (int i = 0; i < item.GetRequiredItems.Count; i++)
                {
                    int x = _playerIM.PlayerItemList.IndexOf(item.GetRequiredItems[i].ToString());
                    _playerIM.PlayerItemList.RemoveAt(x);
                    _playerIM.ButtonRemove(x);
                }
                //Playerにアイテムを付与し、再度購入可能かの判定を行う
                _playerIM.KeyProcess(item);
                CanSellItem();
                break;
        }
    }


    ///// <summary>スプレッドシートからアイテム情報を取ってくる処理</summary>
    //private IEnumerator DataLoad()
    //{
    //    using (UnityWebRequest webRequest = UnityWebRequest.Get(_masterData))
    //    {
    //        yield return webRequest.SendWebRequest();

    //        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
    //        {
    //            Debug.LogError("上手くスプレッドシートを読み込めませんでした" + webRequest.error);
    //        }
    //        else
    //        {
    //            //scriptからのレスポンス取得
    //            string response = webRequest.downloadHandler.text;
    //            ItemDataStorage itemData = JsonUtility.FromJson<ItemDataStorage>(response);

    //            if (_sellItem.Count == 0)
    //            {
    //                _sellItem.Clear();
    //            }
    //            foreach (var d in itemData.Data)
    //            {
    //                var name = d.Name;
    //                GameObject findItem = (GameObject)Resources.Load(name);
    //                _sellItem.Add(findItem);
    //            }

    //            ItemListUp();
    //        }
    //    }
    //}
}

//[Serializable]
//class ItemDataStorage
//{
//    /// <summary>アイテムの番号、名前、価格情報</summary>
//    public ItemData[] Data;
//}

//[Serializable]
//class ItemData
//{
//    public int No;
//    public string Name;
//    public int Price;
//}