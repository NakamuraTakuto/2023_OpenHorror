using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool Is_Flug = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerItemManagement player))
        {
            Is_Flug = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Is_Flug = false;
    }
}
