using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>トレード方法</summary>
public enum TradeType
{
    money,
    item
}

public enum ItemName
{
    Iron,
    BigHunds,
    Wood,
    金属製の箱,
    何かのカギ
}

public abstract class ItemBase : MonoBehaviour
{
    [Tooltip("アイテムのトレード方法")]
    [SerializeField] private TradeType _tradeType = TradeType.money;
    public TradeType GetTradeTyoe => _tradeType;
    [Tooltip("Item名を設定")]
    [SerializeField] private string _itemName;
    public string GetItemName => _itemName;
    [Tooltip("購入時に必要なアイテム")]
    [SerializeField] private List<ItemName> _requiredItems = new();
    public List<ItemName> GetRequiredItems => _requiredItems;
    [Tooltip("購入に必要な金額")]
    [SerializeField] private int _needMoney = 10;
    public int GetNeedMoney => _needMoney;


    private void Start()
    {
        if (_itemName == null || GetItemName == null)
        {
            Debug.Log("Item名がNullです");
        }
    }

    /// <summary>インベントリで選択された時の処理</summary>
    public abstract void Action();

    /// <summary>購入可能かの判定(Playerの所持品, Playerの所持金</summary
    public bool Condition(List<string> plyerItems, int playerMonay)
    {
        switch (_tradeType)
        {
            case TradeType.item:
                var tempList = new List<string>(plyerItems);
                for (int i = 0; i < _requiredItems.Count; i++) //購入に必要なアイテムの分ループ
                {
                    if (!tempList.Contains(_requiredItems[i].ToString()))
                    {//必要なアイテムが一つでもなかったとき、Falseを返してループを終了
                        return false;
                    }
                    else
                    {//必要なアイテムがあったときにコピーしたリストからリムーブして次の判定
                        tempList.RemoveAt(tempList.IndexOf(_requiredItems[i].ToString()));
                    }
                }
                return true;

            case TradeType.money:
                if (playerMonay >= _needMoney)
                {//Playerの金が足りたらtrue,足りなかったらfalse
                    return true;
                }
                else
                {
                    return false;
                }
            default :
                return false;
        }
        
    }

    /// <summary>Itemがインベントリに追加された時に実行, 描画の停止</summary>
    public void ItemOFF() 
    {
        //ColliderとRendererをFalseにして判定の取得と描画を止めている
        if (gameObject.TryGetComponent(out Collider collider))
        {
            collider.enabled = false; 
        }
        if (gameObject.TryGetComponent(out MeshRenderer renderer))
        {
            gameObject.transform.position = Camera.main.transform.position;
            renderer.enabled = false;
        }
        else
        {
            int x = gameObject.transform.childCount;

            for (int i = 0; i < x; i++)
            {
                var v = gameObject.transform.GetChild(i);

                if (v.TryGetComponent(out MeshRenderer vM))
                {
                    gameObject.transform.position = Camera.main.transform.position;
                    vM.enabled = false;
                }

            }
            gameObject.transform.position = Camera.main.transform.position;
        }
    }
}
