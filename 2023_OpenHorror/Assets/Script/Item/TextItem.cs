using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextItem : ItemBase
{
    [Tooltip("�e�L�X�g��\������UIImage")]
    [SerializeField] GameObject _textImageUI = null;
    [Tooltip("�e�L�X�g�̓��e")]
    [SerializeField] string _message = "���b�Z�[�W���ݒ肳��Ă��܂���";

    
    public override void Action()
    {
        _textImageUI.GetComponentInChildren<Text>().text = _message;
        _textImageUI.SetActive(true);
    }
}
