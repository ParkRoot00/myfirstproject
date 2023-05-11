using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//지금 이거 못 만들듯 나중에 하자
[CreateAssetMenu(fileName ="Item",menuName ="Scriptble object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee,Range,Glove,shoe,Heal}

    [Header("# Main")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile;
}