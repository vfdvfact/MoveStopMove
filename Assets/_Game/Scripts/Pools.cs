using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pools : Singleton<Pools>
{
    Dictionary<PoolType, Pool> poolInstance = new Dictionary<PoolType, Pool>();
    [SerializeField] Transform weaponShow;
    [SerializeField] Canvas indicatorParent;
    public GameUnit[] weapons; 
    private void Awake()
    {
        GameUnit[] gameUnits = Resources.LoadAll<GameUnit>("Pool/Bullet");
        weapons = new GameUnit[gameUnits.Length];
        for (int i = 0; i < gameUnits.Length; i++)
        {
            Preload(gameUnits[i], new GameObject(gameUnits[i].name).transform);
            weapons[i]= Instantiate(gameUnits[i], weaponShow);
        }
        gameUnits = Resources.LoadAll<GameUnit>("Pool/NPC");
        for (int i = 0; i < gameUnits.Length; i++)
        {
            Preload(gameUnits[i], new GameObject(gameUnits[i].name).transform);
        }
        GameUnit obj = Resources.Load<GameUnit>("Pool/Indicator/NPCIndicator");
        Preload(obj,indicatorParent.transform);
    }
    public void Preload(GameUnit prefab, Transform parent)
    {
        if (prefab == null)
        {
            Debug.LogError("PREFAB IS EMPTY!!!");
            return;
        }
        if (!poolInstance.ContainsKey(prefab.PoolType) || poolInstance[prefab.PoolType] == null)
        {
            Pool p = new Pool();
            p.PreLoad(prefab, parent);
            poolInstance[prefab.PoolType] = p;
        }
    }
    public T Spawn<T>(PoolType poolType, Vector3 pos, Quaternion rot) where T : GameUnit
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOAD!!!");
            return null;
        }
        return poolInstance[poolType].Spawn(pos, rot) as T;
    }
    public void Despawn(GameUnit unit)
    {
        if (!poolInstance.ContainsKey(unit.PoolType))
        {
            Debug.LogError(unit.PoolType + "IS NOT PRELOAD!!!");
        }
        poolInstance[unit.PoolType].Despawn(unit);
    }
}
public enum PoolType
{
    Bullet1, Bullet2, Bullet3,NPC1,NPC2,NPC3,Indicator,
}
public enum GameState
{
    MainMenu,GamePlay,GamePause,GameVictory,
}
public class Pool
{
    Transform parent;
    GameUnit prefab;
    Queue<GameUnit> inactives = new Queue<GameUnit>();
    List<GameUnit> actives = new List<GameUnit>();
    public void PreLoad(GameUnit prefab, Transform parent)
    {
        this.parent = parent;
        this.prefab = prefab;
    }
    public GameUnit Spawn(Vector3 pos, Quaternion rot)
    {
        GameUnit unit;
        if (inactives.Count <= 0)
        {
            unit = GameObject.Instantiate(prefab, parent);
        }
        else
        {
            unit = inactives.Dequeue();
        }
        unit.TF.SetPositionAndRotation(pos, rot);
        actives.Add(unit);
        unit.gameObject.SetActive(true);
        return unit;
    }
    public void Despawn(GameUnit unit)
    {
        if (unit != null && unit.gameObject.activeSelf)
        {
            actives.Remove(unit);
            inactives.Enqueue(unit);
            unit.gameObject.SetActive(false);
        }
    }
}