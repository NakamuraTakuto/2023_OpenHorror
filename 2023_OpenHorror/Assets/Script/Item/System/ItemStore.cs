using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ItemStore : MonoBehaviour
{
    [Tooltip("Plauer�ɃA�^�b�`����Ă���PlayerItemManagement")]
    [SerializeField] private PlayerItemManagement _playerIM;
    [Tooltip("Player���͈͓��ɂ���Ƃ��ɕ\������ē��p��UI")]
    [SerializeField] private GameObject _infomationUI = null;
    [Tooltip("�A�C�e���X�g�A�Ŕ����Ă���A�C�e��")]
    [SerializeField] private List<GameObject> _sellItem = new();
    [Tooltip("�A�C�e���V���b�v���J���ꂽ���ɂ���Panel(LayoutGroup����)")]
    [SerializeField] private GameObject _shopPanel = null;
    [Tooltip("�V���b�v��panel�ɃC���X�^���X����Button")]
    [SerializeField] private GameObject _button;
   // [SerializeField] private string _masterData = "https://script.google.com/macros/s/AKfycbybuatODf8U5GJ6v1NlIdGtglnMFFwxRhBFeWI4ywdunqlKUOS_1lu3BTjmVcvWwFqFGA/exec";
    private Dictionary<string, GameObject> _sellDic = new();
    private Dictionary<string, GameObject> _buttonDic = new();
    private TradeType _tradeType = TradeType.money;

    //private void Awake()
    //{
    //    StartCoroutine("DataLoad");
    //}

    private void Start()
    {
        if (_shopPanel == null || _infomationUI == null)
        {
            Debug.Log("�A�^�b�`����Ă��Ȃ����̂�����܂�");
        }
        ItemListUp();
    }
    private void Update()
    {
        //ItemStore���J���ꂽ���Ɏ��s����
        if (Input.GetKeyDown(KeyCode.F) && _playerIM != null) { PanelControl(); }

        //Plauer���o�b�N���J�����Ƃ���off�ɂ���
        if (Input.GetKeyDown(KeyCode.B)){ _infomationUI.SetActive(false); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerItemManagement player))
        {
            //Player���͈͓��ɂ���Ƃ��Ɉē��pUI��\������
            _infomationUI.SetActive(true);

            //�擾�����X�N���v�g��ݒ肷��
            _playerIM = player;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //�ē��p�t�h�̕\�����~�߂�
        _infomationUI.SetActive(false);

        //�擾���Ă����X�N���v�g��j������
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

    /// <summary>�̔�����A�C�e�����X�g�A�ɕ��ׂ�</summary>
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
        else { Debug.Log("���X�g��Null�̂��ߕ��ׂ�ꂦ�Ă��܂���"); }
    }

    /// <summary>�����Ă���A�C�e�����w���\��Ԃɂ��邩�̔���</summary>
    private void CanSellItem()
    {
        //�����Ă���A�C�e���̌��񔻒胋�[�v
        for (int i = 0; i < _sellItem.Count; i++)
        {
            var sellItemScript = _sellItem[i].GetComponent<ItemBase>();

            //�w���s�̏�Ԃɂ���Ƃ���Button���g�p�s�ɂ���
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
    
    /// <summary>�w������</summary>
    public void Trade(string itemName)
    { 
        var item = _sellDic[itemName].GetComponent<ItemBase>();
        _tradeType = item.GetTradeTyoe;

        switch (_tradeType)
        {
            case TradeType.money:
                _playerIM.PlayerMoney -= item.GetNeedMoney;
                CanSellItem();
                _playerIM.KeyProcess(item);
                break;

            case TradeType.item:
                for (int i = 0; i < item.GetRequiredItems.Count; i++)
                {
                    int x = _playerIM.PlayerItemList.IndexOf(item.GetRequiredItems[i].ToString());
                    _playerIM.PlayerItemList.RemoveAt(x);
                    _playerIM.ButtonRemove(x);
                }
                _playerIM.KeyProcess(item);
                CanSellItem();
                break;
        }
    }


    ///// <summary>�X�v���b�h�V�[�g����A�C�e����������Ă��鏈��</summary>
    //private IEnumerator DataLoad()
    //{
    //    using (UnityWebRequest webRequest = UnityWebRequest.Get(_masterData))
    //    {
    //        yield return webRequest.SendWebRequest();

    //        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
    //        {
    //            Debug.LogError("��肭�X�v���b�h�V�[�g��ǂݍ��߂܂���ł���" + webRequest.error);
    //        }
    //        else
    //        {
    //            //script����̃��X�|���X�擾
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
//    /// <summary>�A�C�e���̔ԍ��A���O�A���i���</summary>
//    public ItemData[] Data;
//}

//[Serializable]
//class ItemData
//{
//    public int No;
//    public string Name;
//    public int Price;
//}