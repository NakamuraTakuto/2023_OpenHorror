using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemBase : MonoBehaviour
{
    [Header("Item����ݒ�")]
    [SerializeField] private string _itemName;
    public string GetItemName => _itemName;

    private void Start()
    {
        if (_itemName == null || GetItemName == null)
        {
            Debug.Log("Item����Null�ł�");
        }
    }

    public abstract void Action(); //�C���x���g���őI�����ꂽ���̏���

    public void ItemOFF() //Item���C���x���g���ɒǉ����ꂽ���Ɏ��s
    {
        //Collider��Renderer��False�ɂ��Ĕ���̎擾�ƕ`����~�߂Ă���
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }
}
