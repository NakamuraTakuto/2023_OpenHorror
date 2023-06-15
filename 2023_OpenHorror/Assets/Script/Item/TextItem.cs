using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextItem : ItemBase
{
    [Tooltip("テキストを表示するUIImage")]
    [SerializeField] GameObject _textImageUI = null;
    [Tooltip("テキストの内容")]
    [SerializeField] string _message = "メッセージが設定されていません";

    
    public override void Action()
    {
        _textImageUI.GetComponentInChildren<Text>().text = _message;
        _textImageUI.SetActive(true);
    }
}
