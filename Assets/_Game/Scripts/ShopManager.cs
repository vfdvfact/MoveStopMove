using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] Button skinLeft;
    [SerializeField] Button skinRight;
    [SerializeField] Button skinBuy;
    [SerializeField] Button skinEquip;
    [SerializeField] Button skinClose;
    [SerializeField] Button weaponLeft;
    [SerializeField] Button weaponRight;
    [SerializeField] Button weaponBuy;
    [SerializeField] Button weaponEquip;
    [SerializeField] Button weaponClose;
    [SerializeField] Button skinShop;
    [SerializeField] Button weaponShop;
    [SerializeField] Button play;
    [SerializeField] Button pause;
    [SerializeField] Button mainMenu;
    [SerializeField] Button resume;
    [SerializeField] Button failClose;
    [SerializeField] Button failRevive;
    [SerializeField] Button failMainMenu;
    [SerializeField] Button victoryPlayAgain;
    [SerializeField] Button victoryNextStage;
    [SerializeField] int reviveCost=1;
    [SerializeField] PlayerAction player;
    [SerializeField] CanvasWeaponShop weaponShopManager;
    [SerializeField] CanvasSkinShop skinShopManager;
    [SerializeField] CanvasFail failManager;
    [SerializeField] CameraFollower mainCamera;
    [SerializeField] StageManager stageManager;
    [SerializeField] NPCSpawner spawner;
    [SerializeField] GameObject weaponShow;
    Transform[] weapons;
    private float speed=100f;

    void Start()
    {
        weapons = new Transform[Pools.Instance.weapons.Length];
        for (int i = 0; i < Pools.Instance.weapons.Length; i++)
        {
            weapons[i]= Pools.Instance.weapons[i].GetComponent<Transform>();
        }
        skinLeft.onClick.AddListener(player.DecreaseSkinIndex);
        skinLeft.onClick.AddListener(skinShopManager.LeftAndRightButton);
        skinRight.onClick.AddListener(player.IncreaseSkinIndex);
        skinRight.onClick.AddListener(skinShopManager.LeftAndRightButton);
        skinBuy.onClick.AddListener(skinShopManager.BuyButton);
        skinEquip.onClick.AddListener(player.SkinEquip);
        skinEquip.onClick.AddListener(skinShopManager.EquipButton);
        skinClose.onClick.AddListener(player.ChangeSkinForPlay);
        skinClose.onClick.AddListener(CloseButton);
        weaponLeft.onClick.AddListener(DecreaseWeaponIndex);
        weaponLeft.onClick.AddListener(weaponShopManager.LeftAndRightButton);
        weaponRight.onClick.AddListener(IncreaseWeaponIndex);
        weaponRight.onClick.AddListener(weaponShopManager.LeftAndRightButton);
        weaponBuy.onClick.AddListener(weaponShopManager.BuyButton);
        weaponEquip.onClick.AddListener(player.WeaponEquip);
        weaponEquip.onClick.AddListener(weaponShopManager.EquipButton);
        weaponClose.onClick.AddListener(player.ChangeWeapon);
        weaponClose.onClick.AddListener(WeaponShopClose);
        skinShop.onClick.AddListener(SkinShopOpen);
        skinShop.onClick.AddListener(skinShopManager.SkinShopButton);
        weaponShop.onClick.AddListener(WeaponShopOpen);
        weaponShop.onClick.AddListener(weaponShopManager.WeaponShopButton);
        play.onClick.AddListener(PlayGame);
        pause.onClick.AddListener(PauseGame);
        mainMenu.onClick.AddListener(MainMenu);
        resume.onClick.AddListener(Resume);
        failRevive.onClick.AddListener(ReviveButton);
        failClose.onClick.AddListener(FailCloseButton);
        failMainMenu.onClick.AddListener(FailMainMenuButton);
        victoryPlayAgain.onClick.AddListener(VictoryPlayAgainButton);
        victoryNextStage.onClick.AddListener(VictoryNextStageButton);
    }
    void VictoryNextStageButton()
    {
        stageManager.NextGame();
        player.ResetPosition();
        GameManager.Instance.State=GameState.GamePlay;
    }
    void VictoryPlayAgainButton()
    {
        UIManager.Instance.CloseUI<CanvasVictory>(0);
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        GameManager.Instance.State = GameState.MainMenu;
        player.ResetPosition();
    }
    void FailMainMenuButton()
    {
        UIManager.Instance.CloseUI<CanvasFail>(0);
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        GameManager.Instance.State = GameState.MainMenu;
        player.ResetPosition();
    }
    void FailCloseButton()
    {
        failManager.CloseButton();
    }
    void ReviveButton()
    {
        if (player.playerData.coin >= reviveCost)
        {
            player.playerData.coin-= reviveCost;
            failManager.ReviveButton();
            player.Die();
        }

    }
    void CloseButton()
    {
        skinShopManager.CloseButton();
        mainCamera.ChangeCameraMode(1);
        SaveManager.Instance.SaveGame();
    }
    void Resume()
    {
        UIManager.Instance.CloseUI<CanvasGamePause>(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        GameManager.Instance.State = GameState.GamePlay;
    }
    void MainMenu()
    {
        UIManager.Instance.CloseUI<CanvasGamePause>(0);
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        GameManager.Instance.State = GameState.MainMenu;
        player.ResetPosition();
    }
    void PauseGame()
    {
        UIManager.Instance.CloseUI<CanvasGamePlay>(0);
        UIManager.Instance.OpenUI<CanvasGamePause>();
        GameManager.Instance.State = GameState.GamePause;
    }
    void PlayGame()
    {
        UIManager.Instance.CloseUI<CanvasMainMenu>(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        GameManager.Instance.State = GameState.GamePlay;
        stageManager.PlayGame();
        player.ResetPosition();
    }
    void SkinShopOpen()
    {
        player.ChangeSkinForPlay();
        UIManager.Instance.CloseUI<CanvasMainMenu>(0);
        UIManager.Instance.OpenUI<CanvasSkinShop>();
        mainCamera.ChangeCameraMode(3);
    }
    void WeaponShopOpen()
    {
        UIManager.Instance.CloseUI<CanvasMainMenu>(0);
        UIManager.Instance.OpenUI<CanvasWeaponShop>();
        mainCamera.ChangeCameraMode(2);
        weaponShow.SetActive(true);
        ShowWeapon(player.weaponIndex);
    }
    void WeaponShopClose()
    {
        weaponShopManager.CloseButton();
        weaponShow.SetActive(false);
        mainCamera.ChangeCameraMode(1);
        SaveManager.Instance.SaveGame();
    }
    public void OnInit()
    {

        ShowWeapon(player.weaponIndex);
    }
    public void ShowWeapon(int type)
    {
        weapons[type].gameObject.SetActive(true);
    }
    public void HideWeapon(int type)
    {
        weapons[player.weaponIndex].gameObject.SetActive(false);
    }
    public void IncreaseWeaponIndex()
    {
        HideWeapon(player.weaponIndex);
        if (player.weaponIndex < weapons.Length - 1)
        {
            player.weaponIndex = player.weaponIndex + 1;
        }
        else
        {
            player.weaponIndex = 0;
        }
        ShowWeapon(player.weaponIndex);
    }
    public void DecreaseWeaponIndex()
    {
        HideWeapon(player.weaponIndex);
        if (player.weaponIndex == 0)
        {
            player.weaponIndex = weapons.Length - 1;
        }
        else
        {
            player.weaponIndex = player.weaponIndex - 1;
        }
        ShowWeapon(player.weaponIndex);
    }
    private void Update()
    {
        if (weapons[player.weaponIndex].gameObject.activeSelf)
        {
            weapons[player.weaponIndex].Rotate(0f, speed * Time.deltaTime, 0f);
        }
    }
}
