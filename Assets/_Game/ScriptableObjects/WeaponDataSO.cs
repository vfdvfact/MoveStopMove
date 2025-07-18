using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType
{
    Axe = 0, Uzi = 1, Sword = 2,
}
[CreateAssetMenu(menuName = "WeaponDataSO")]
public class WeaponDataSO : ScriptableObject
{
    public WeaponInfo[] weapons;
}
[System.Serializable]
public class WeaponInfo
{
    public int cost;
    public bool isBought;
    public Gun weapon;
    public string description;
    public string name;
}
