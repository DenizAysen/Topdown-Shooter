using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pool 
{
    public string Name { get => Prefab.name; }
    public GameObject Prefab;
    public int Size;
}
