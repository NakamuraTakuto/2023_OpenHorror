using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class KeyItem : ItemBase
{
    [Tooltip("テキストを表示するUIImage")]
    [SerializeField] GameObject _textImageUI = null;
    [Tooltip("テキストの内容")]
    [SerializeField] string _message = "メッセージが設定されていません";
    [SerializeField] string _itemText;
    Goal _goal = null;
    public override void Action()
    {
        if (FindObjectOfType<KeyItem>() == null)
        {
            Instantiate(gameObject);
            _goal = FindObjectOfType<Goal>();
        }

        if (_goal.Is_Flug)
        {
            FindObjectOfType<GameManager>().Is_Clear = true;
        }
        else
        {
            _textImageUI.GetComponentInChildren<Text>().text = _message;
            _textImageUI.SetActive(true);
        }
    }
}
