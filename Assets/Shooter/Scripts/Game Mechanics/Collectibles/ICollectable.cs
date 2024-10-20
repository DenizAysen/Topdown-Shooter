using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  static CommonVariables;
public interface ICollectable 
{
    public CollectableType ItemType { get; set; }
    void Collected();
}
