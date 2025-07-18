using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StageManager : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    GameObject[] stage;
    [SerializeField] NPCSpawner spawner;
    int currentStage = 1;

    private void Awake()
    {
        GameObject[] obj= Resources.LoadAll<GameObject>("Stage");
        stage = new GameObject[obj.Length];
        for (int i = 0; i < obj.Length; i++)
        {
            stage[i] = Instantiate(obj[i]);
            if(i!=0)
            {
                stage[i].SetActive(false);
            }

        }
    }
    public void PlayGame()
    {
        currentStage = 1;
        navMeshSurface.BuildNavMesh();
        spawner.StorePosition();
        spawner.FirstSpawn();
    }
    public void NextGame()
    {
        stage[currentStage-1].SetActive(false);
        currentStage++;
        stage[currentStage-1].SetActive(true);
        UIManager.Instance.CloseUI<CanvasVictory>(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        navMeshSurface.BuildNavMesh();
        spawner.StorePosition();
        spawner.FirstSpawn();
    }
}
