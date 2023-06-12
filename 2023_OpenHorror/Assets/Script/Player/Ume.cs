using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ume
{
    [SerializeField] int count;
    [SerializeField] string name;

    public Ume(int count, string name)
    {
        this.count = count;
        this.name = name;
    }
}
