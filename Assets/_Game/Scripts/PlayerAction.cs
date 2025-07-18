using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAction : Character
{
    public Gun[] guns;
    public JoystickControl joyStick;
    public Transform detectCircle;
    [SerializeField] Vector2 offsetDetection = new Vector2(2.3f,2.3f);
    [SerializeField] float scaleMultiplier=1.2f;
    public float speed = 5f;
    public int skinIndex = 0;
    public int weaponIndex = 0;
    public Transform startPoint;
    public PlayerDataSO playerData;
    [SerializeField] SkinDataSO skinData;
    [SerializeField] Vector3 markPosition = new Vector3(0, 0.1f, 0); 
    [SerializeField] Transform mark;
    [SerializeField] Renderer skin;
    [SerializeField] Vector3 originalDir=new Vector3(0,0,1f);
    Vector3 originalScale;
    float originalDetectionRadius;

    public override void OnEnable()
    {
        base.OnEnable();
        TF.position = startPoint.position;
        ChangeState(new PlayerIdleState());
    }
    public override void AddPoint()
    {
        base.AddPoint();
        transform.localScale *= scaleMultiplier;
        detectionRadius *= scaleMultiplier;
        if (point >= pointMax)
        {
            UIManager.Instance.OpenUI<CanvasVictory>();
            UIManager.Instance.CloseUI<CanvasGamePlay>(0);
            ChangeState(new PlayerVictoryState());
            GameManager.Instance.State=GameState.GameVictory;
        }
    }
    void ShowRange()
    {
        detectCircle.localScale = new Vector3(offsetDetection.x, offsetDetection.y,detectCircle.localScale.y);
    }
    void ShowTarget()
    {
        if (GetFilteredCollider()!=null)
        {
            mark.SetParent(target.transform);
            mark.localPosition = markPosition;
            mark.gameObject.SetActive(true);
        }
        else
        {
            mark.SetParent(null);
            mark.gameObject.SetActive(false);
        }

    }
    public void ResetPosition()
    {
        TF.position = startPoint.position;
        ChangeState(new PlayerIdleState());
        transform.localScale = originalScale;
        detectionRadius = originalDetectionRadius;
        hp = maxHp;
        isDead = false;
        point = 0;
        LookAt(originalDir);
    }
    public override void InstanGunFromResources()
    {
        guns = new Gun[weaponData.weapons.Length];
        for (int i = 0; i < weaponData.weapons.Length; i++) 
        {
            guns[i] = Instantiate(weaponData.weapons[i].weapon, gunHoldingHand);
        }
        currentGun = guns[playerData.selectedGun];
        currentGun.gameObject.SetActive(true);
        ChangeSkinForPlay();
    }
    public void ChangeSkinForShop()
    {
        skin.material = skinData.shorts[skinIndex].mat;
    }
    public void ChangeSkinForPlay()
    {
        skin.material = skinData.shorts[playerData.selectedSkin].mat;
    }
    public void ChangeWeapon()
    {
        currentGun.gameObject.SetActive(false);
        currentGun = guns[playerData.selectedGun];
        currentGun.gameObject.SetActive(true);
    }
    public void IncreaseSkinIndex()
    {
        if(skinIndex< skinData.shorts.Length-1)
        {
            skinIndex = skinIndex + 1;
        }
        else
        {
            skinIndex = 0;
        }
        ChangeSkinForShop();
    }
    public void DecreaseSkinIndex()
    {
        if (skinIndex == 0)
        {
            skinIndex = skinData.shorts.Length-1;
        }
        else
        {
            skinIndex = skinIndex - 1;
        }
        Debug.Log("ChangeSkin");
        ChangeSkinForShop();
    }
    public void WeaponBuy()
    {
        playerData.coin = playerData.coin - weaponData.weapons[weaponIndex].cost;
        weaponData.weapons[weaponIndex].isBought = true;
        playerData.selectedGun = weaponIndex;
    }
    public void SkinBuy()
    {
        playerData.coin = playerData.coin - skinData.shorts[skinIndex].cost;
        skinData.shorts[skinIndex].isBought = true;
        playerData.selectedSkin = skinIndex;
    }
    public void SkinEquip()
    {
        playerData.selectedSkin = skinIndex;
    }
    public void WeaponEquip()
    {
        playerData.selectedGun = weaponIndex;
    }
    public override void Die()
    {
        NPCSpawner.Instance.SpawnPlayerWithWaiting();
        gameObject.SetActive(false);
    }
    public override void Move()
    {
        TF.Translate(joyStick.direct * speed * Time.deltaTime, Space.World);
        LookAt(joyStick.direct);
    }
    public override void WaitFailUI()
    {
        pastTime = pastTime + Time.deltaTime;
        if (pastTime >= timeLimit)
        {
            UIManager.Instance.OpenUI<CanvasFail>();
            UIManager.Instance.CloseUI<CanvasGamePlay>(0);
        }
    }
    public override bool CalcuDameAndDie(float dama)
    {
        if (hp <= 0)
            return false;
        hp = hp - dama;
        if (hp <= 0)
        {
            ChangeState(new PlayerDieState());
            return true;
        }
        else
        {
            return false;
        }
    }
    //------------------------------------------------------------------------------------------------------------
    public override void Start()
    {
        base.Start();    
        detectionLayer = LayerMask.GetMask("NPC");
        originalScale = transform.localScale;
        originalDetectionRadius = detectionRadius;
    }
    public override void Update()
    {
        base.Update();
        ShowTarget();
        ShowRange();
    }
    public override bool GetInputForMove()
    {
        if (Mathf.Abs(joyStick.direct.z) < 0.01f &&
            Mathf.Abs(joyStick.direct.x) < 0.01f)
        {
            return false;
        }
        return true;
    }
}
