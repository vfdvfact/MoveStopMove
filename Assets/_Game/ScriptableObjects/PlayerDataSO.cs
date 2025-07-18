using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerDataSO")]
public class PlayerDataSO : ScriptableObject
{
    public int coin;
    public int selectedSkin;
    public int selectedGun;
}
