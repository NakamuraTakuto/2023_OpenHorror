using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottonTestSystem : MonoBehaviour
{
    private Button button = default;

    private void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() => Test());
    }

    public void Test()
    {
        Debug.Log("click!!");
    }
}
