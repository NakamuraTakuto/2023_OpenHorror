using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class KeyItem : ItemBase
{
    [Tooltip("�e�L�X�g��\������UIImage")]
    [SerializeField] GameObject _textImageUI = null;
    [Tooltip("�e�L�X�g�̓��e")]
    [SerializeField] string _message = "���b�Z�[�W���ݒ肳��Ă��܂���";
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
