using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemBase : MonoBehaviour
{
    [Tooltip("Item����ݒ�")]
    [SerializeField] private string _itemName;
    public string GetItemName => _itemName;
    [Tooltip("�w�����ɕK�v�ȃA�C�e��")]
    [SerializeField] public List<string> RequiredItems = new();

    private void Start()
    {
        if (_itemName == null || GetItemName == null)
        {
            Debug.Log("Item����Null�ł�");
        }
    }

    public abstract void Action(); //�C���x���g���őI�����ꂽ���̏���

    public bool Condition(List<string> plyerItems)
    {
        for (int i = 0; i < RequiredItems.Count; i++)
        {
            if (!plyerItems.Contains(RequiredItems[i]))
            {
                return false;
            }
            else
            {
                plyerItems.RemoveAt(plyerItems.IndexOf(RequiredItems[i]));
            }
        }
        return true;
    }

    public void ItemOFF() //Item���C���x���g���ɒǉ����ꂽ���Ɏ��s
    {
        //Collider��Renderer��False�ɂ��Ĕ���̎擾�ƕ`����~�߂Ă���
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }
}
