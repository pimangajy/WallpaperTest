using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/Item Data")]
public class ItemData : ScriptableObject
{
    public enum ItemType
    {
        expIncrease,
        interestIncrease,
        typeChanged
    }

    public string itemName;
    [TextArea(5, 10)]
    public string itemExplanation;
    public Sprite itemIcon;
    public Sprite itemHideIcon;

    public ItemType itemType;
    public int value;
}