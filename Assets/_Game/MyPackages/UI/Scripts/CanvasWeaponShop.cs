using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasWeaponShop : UICanvas
{
    [SerializeField] PlayerAction player;
    [SerializeField] GameObject buyButtonCanvas;
    [SerializeField] GameObject equipButtonCanvas;
    [SerializeField] GameObject closeButtonCanvas;
    [SerializeField] GameObject equippedCanvas;
    [SerializeField] WeaponDataSO weaponData;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI describedText;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI coinText;
    public void LeftAndRightButton()
    {
        if (player.playerData.selectedGun == player.weaponIndex)
        {
            equippedCanvas.SetActive(true);
            equipButtonCanvas.SetActive(false);
            buyButtonCanvas.SetActive(false);
            ChangeNameAndDescription();
        }
        else
        {
            if (weaponData.weapons[player.weaponIndex].isBought)
            {
                equipButtonCanvas.SetActive(true);
                equippedCanvas.SetActive(false);
                buyButtonCanvas.SetActive(false);
                ChangeNameAndDescription();
            }
            else
            {
                buyButtonCanvas.SetActive(true);
                costText.text = weaponData.weapons[player.weaponIndex].cost.ToString();
                equipButtonCanvas.SetActive(false);
                equippedCanvas.SetActive(false);
                ChangeNameAndDescription();
            }
        }
    }
    public void CloseButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
    public void BuyButton()
    {
        if (player.playerData.coin >= weaponData.weapons[player.weaponIndex].cost)
        {
            player.WeaponBuy();
            buyButtonCanvas.SetActive(false);
            equippedCanvas.SetActive(true);
            equipButtonCanvas.SetActive(false);
            coinText.text = player.playerData.coin.ToString();
        }
    }
    public void EquipButton()
    {
        equipButtonCanvas.SetActive(false);
        equippedCanvas.SetActive(true);
        buyButtonCanvas.SetActive(false);
    }
    public void ChangeNameAndDescription()
    {
        nameText.text = weaponData.weapons[player.weaponIndex].name;
        describedText.text = weaponData.weapons[player.weaponIndex].description;
    }
    public void WeaponShopButton()
    {
        coinText.text = player.playerData.coin.ToString();
        LeftAndRightButton();
    }
}
