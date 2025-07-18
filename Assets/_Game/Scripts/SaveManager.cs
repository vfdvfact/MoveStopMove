using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private string savePath;
    public SkinDataSO skinData;
    public WeaponDataSO weaponData;
    public PlayerDataSO playerData;

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/savefile.json";
        LoadGame();
    }
    public void SaveGame()
    {
        DataForSave data = new DataForSave();
        data.playerCoin = playerData.coin;
        data.playerSelectedGun = playerData.selectedGun;
        data.playerSelectedSkin = playerData.selectedSkin;
        bool[] skins=new bool[skinData.shorts.Length];
        for (int i = 0; i < skinData.shorts.Length; i++)
        {
            skins[i] = skinData.shorts[i].isBought;
        }
        data.skinIsBought = skins;
        bool[] weapons = new bool[weaponData.weapons.Length];
        for (int i = 0; i < weaponData.weapons.Length; i++)
        {
            weapons[i] = weaponData.weapons[i].isBought;
        }
        data.gunIsBought = weapons;
        string json = JsonUtility.ToJson(data, true); // "true" makes it pretty-printed
        File.WriteAllText(savePath, json);

        Debug.Log("Game Saved: " + savePath);
    }
    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            DataForSave data = JsonUtility.FromJson<DataForSave>(json);

            playerData.coin = data.playerCoin;
            playerData.selectedGun = data.playerSelectedGun;
            playerData.selectedSkin = data.playerSelectedSkin;
            for (int i = 0; i < weaponData.weapons.Length; i++)
            {
                weaponData.weapons[i].isBought = data.gunIsBought[i];
            }
            for (int i = 0; i < skinData.shorts.Length; i++)
            {
                skinData.shorts[i].isBought = data.skinIsBought[i];
            }
        }
        else
        {
            Debug.LogWarning("Save file not found!");
        }
    }

}
