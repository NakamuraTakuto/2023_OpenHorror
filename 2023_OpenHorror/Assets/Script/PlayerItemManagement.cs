using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemManagement : MonoBehaviour
{
    [Header("Player‚ÌItemƒCƒ“ƒxƒ“ƒgƒŠ")]
    [SerializeField] GameObject _itemCanvas;
    [Header("ItemButtonPreFab")]
    [SerializeField] GameObject _itemButton;
    [SerializeField] List<GameObject> _itemList = new();

    void ClickProcess()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.TryGetComponent<ItemBase>(out ItemBase itemBase))
            {
                if (itemBase.GetItemName == null)
                {
                    Debug.Log("Null");
                }
                else
                {
                    var InstantiateObj = Instantiate(_itemButton, _itemCanvas.transform);
                    InstantiateObj.GetComponent<Button>().onClick.AddListener(() => itemBase.Action());
                    InstantiateObj.GetComponentInChildren<Text>().text = itemBase.GetItemName;
                    _itemList.Add(InstantiateObj);
                    hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    hit.collider.gameObject.transform.position = Camera.main.transform.position;
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            ClickProcess();
        }
    }
}
