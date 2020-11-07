using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ItemData
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;
    [TextArea]
    public string itemInfo;

    public bool equip;
    [Header("装备加成")]
    public float health;
    public float poisonResistance;
    public float hitResistance;
    public float magicResistance;
    public float attack;
    public float critRate;
    public float critPower;
    public float power;
    public float magic;
}
