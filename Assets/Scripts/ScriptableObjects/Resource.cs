using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Resource",menuName = "ScriptableObjects/Resource",order =2)]
public class Resource : ScriptableObject
{
    public string name;
    public int quantity;
    public Sprite icon;
}
