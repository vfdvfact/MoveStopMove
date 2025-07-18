using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    public void PlayButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }
    public void SkinShopButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasSkinShop>();
    }
    public void WeaponShopButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasWeaponShop>();
    }
}
