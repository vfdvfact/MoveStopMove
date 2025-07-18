using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    Dictionary<System.Type,UICanvas> canvasActives=new Dictionary<System.Type,UICanvas>();
    //Dictionary<System.Type, UICanvas> canvasPrefabs = new Dictionary<System.Type, UICanvas>();
    //[SerializeField] Transform parent;
    [SerializeField] CanvasFail failUI;
    [SerializeField] CanvasGamePause pauseUI;
    [SerializeField] CanvasGamePlay playUI;
    [SerializeField] CanvasMainMenu menuUI;
    [SerializeField] CanvasSkinShop skinShopUI;
    [SerializeField] CanvasVictory victoryUI;
    [SerializeField] CanvasWeaponShop weaponShopUI;
    public void ReferUI()
    {
        canvasActives[typeof(CanvasFail)] = failUI;
        canvasActives[typeof(CanvasGamePause)] = pauseUI;
        canvasActives[typeof(CanvasGamePlay)] = playUI;
        canvasActives[typeof(CanvasMainMenu)] = menuUI;
        canvasActives[typeof(CanvasSkinShop)] = skinShopUI;
        canvasActives[typeof(CanvasVictory)] = victoryUI;
        canvasActives[typeof(CanvasWeaponShop)] = weaponShopUI;
    }
    private void Awake()
    {
        ReferUI();
        /*UICanvas[] prefabs = Resources.LoadAll<UICanvas>("UI/");
        for (int i = 0; i < prefabs.Length; i++)
        {
            canvasPrefabs.Add(prefabs[i].GetType(), prefabs[i]);
        }*/
    }
    public U OpenUI<U>() where U : UICanvas
    {
        //U canvas = GetUI<U>();
        U canvas = canvasActives[typeof(U)] as U;
        canvas.Setup();
        canvas.Open();
        return canvas;
    }
    public void CloseUI<U>(float time) where U : UICanvas
    {
        if (IsOpened<U>())
        {
            canvasActives[typeof(U)].Close(time);
        }
    }
    public void CloseUIDirectly<U>() where U : UICanvas
    {
        if (IsOpened<U>())
        {
            canvasActives[typeof(U)].CloseDirectly();
        }
    }
    public bool IsLoaded<U>() where U : UICanvas
    {
        return canvasActives.ContainsKey(typeof(U)) && canvasActives[typeof(U)] != null;
    }
    public bool IsOpened<U>() where U : UICanvas
    {
        return IsLoaded<U>() && canvasActives[typeof(U)].gameObject.activeSelf;
    }
    /*public U GetUI<U>() where U : UICanvas
    {
        if (!IsLoaded<U>())
        {
            U prefab = GetUIPrefab<U>();
            U canvas=Instantiate(prefab,parent);
            canvasActives[typeof(U)] = canvas;
        }
        return canvasActives[typeof(U)] as U;           
    }
    U GetUIPrefab<U>() where U : UICanvas
    {
        return canvasPrefabs[typeof(U)] as U;
    }
    */
    public void CloseAll()
    {
        foreach (var canvas in canvasActives)
        {
            if (canvas.Value!=null&&canvas.Value.gameObject.activeSelf)
            {
                canvas.Value.Close(0);
            }
        }
    }
}
