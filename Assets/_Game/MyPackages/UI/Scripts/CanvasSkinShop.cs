using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasSkinShop : UICanvas
{
    [SerializeField] PlayerAction player;
    [SerializeField] GameObject buyButtonCanvas;
    [SerializeField] GameObject equipButtonCanvas;
    [SerializeField] GameObject closeButtonCanvas;
    [SerializeField] GameObject equippedCanvas;
    [SerializeField] SkinDataSO skinData;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI coinText;
    public void LeftAndRightButton()
    {
        if (player.playerData.selectedSkin == player.skinIndex)
        {
            equippedCanvas.SetActive(true);
            equipButtonCanvas.SetActive(false);
            buyButtonCanvas.SetActive(false);
        }
        else
        {
            if (skinData.shorts[player.skinIndex].isBought)
            {
                equipButtonCanvas.SetActive(true);
                equippedCanvas.SetActive(false);
                buyButtonCanvas.SetActive(false);
            }
            else
            {
                buyButtonCanvas.SetActive(true);
                costText.text = skinData.shorts[player.skinIndex].cost.ToString();
                equipButtonCanvas.SetActive(false);
                equippedCanvas.SetActive(false);
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
        if (player.playerData.coin >= skinData.shorts[player.skinIndex].cost)
        {
            player.SkinBuy();
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
    public void SkinShopButton()
    {
        coinText.text = player.playerData.coin.ToString();
    }
}
