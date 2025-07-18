using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkinType
{
    Dabao=0,Onion=1,Vantim=2,
}
[CreateAssetMenu(menuName = "SkinDataSO")]
public class SkinDataSO : ScriptableObject
{
    public ShortInfo[] shorts;
}
[System.Serializable]
public class ShortInfo
{
    public int cost;
    public bool isBought;
    public Material mat;
}
