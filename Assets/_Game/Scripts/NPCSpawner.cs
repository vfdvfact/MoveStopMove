using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCSpawner : Singleton<NPCSpawner>
{
    public float nPC1Number = 1;
    public float nPC2Number = 1;
    public float nPC3Number = 1;
    public Queue<Vector3> randomPositions = new Queue<Vector3>();
    public List<Vector3> usedPositions = new List<Vector3>();
    public float minDistance = 10f;
    public int maxAttempts = 10;
    public Transform player;
    [SerializeField] NPCIndicatorManager indicatorManager;
    [SerializeField] Vector3 nPCRotation=new Vector3(90f,0f,0f);
    public Vector3 GetRandomPointWithSpacing()
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 candidate = GetRandomPointOnNavMesh();

            bool tooClose = false;
            foreach (Vector3 pos in usedPositions)
            {
                if (Vector3.Distance(candidate, pos) < minDistance)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
            {
                usedPositions.Add(candidate);
                return candidate;
            }
        }

        // If no valid point is found after several attempts, fallback to any point
        Vector3 fallback = GetRandomPointOnNavMesh();
        usedPositions.Add(fallback);
        Debug.Log("no valid point");
        return fallback;
    }
    public Vector3 GetRandomPointOnNavMesh()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        // Pick a random triangle
        int triangleIndex = Random.Range(0, navMeshData.indices.Length / 3) * 3;

        // Get the 3 vertices of that triangle
        Vector3 vertex1 = navMeshData.vertices[navMeshData.indices[triangleIndex]];
        Vector3 vertex2 = navMeshData.vertices[navMeshData.indices[triangleIndex + 1]];
        Vector3 vertex3 = navMeshData.vertices[navMeshData.indices[triangleIndex + 2]];

        // Pick a random point inside that triangle using barycentric coordinates
        Vector3 randomPoint = RandomPointInTriangle(vertex1, vertex2, vertex3);

        return randomPoint;
    }

    private Vector3 RandomPointInTriangle(Vector3 a, Vector3 b, Vector3 c)
    {
        float r1 = Random.value;
        float r2 = Random.value;

        // Make sure the point lies inside the triangle
        if (r1 + r2 > 1f)
        {
            r1 = 1f - r1;
            r2 = 1f - r2;
        }

        return a + r1 * (b - a) + r2 * (c - a);
    }
    private void Start()
    {

    }
    public void FirstSpawn()
    {
        for (int i = 0; i < nPC1Number; i++)
        {
            Spawn1();
        }
        for (int i = 0; i < nPC2Number; i++)
        {
            Spawn2();
        }
        for (int i = 0; i < nPC3Number; i++)
        {
            Spawn3();
        }
    }
    void SpawnPlayer()
    {
        player.gameObject.SetActive(true);
    }
    void Spawn1()
    {        
        GameObject obj = Pools.Instance.Spawn<NPC0>(PoolType.NPC1, GetPosition(), Quaternion.identity).gameObject;
        indicatorManager.AddNPC(obj.GetComponent<Transform>());
        obj.GetComponent<NPC0>().indicatorManager = indicatorManager;
    }
    void Spawn2()
    {
        GameObject obj = Pools.Instance.Spawn<NPC0>(PoolType.NPC2, GetPosition(), Quaternion.identity).gameObject;
        indicatorManager.AddNPC(obj.GetComponent<Transform>());
        obj.GetComponent<NPC0>().indicatorManager = indicatorManager;
    }
    void Spawn3()
    {
        GameObject obj = Pools.Instance.Spawn<NPC0>(PoolType.NPC3, GetPosition(), Quaternion.identity).gameObject;
        indicatorManager.AddNPC(obj.GetComponent<Transform>());
        obj.GetComponent<NPC0>().indicatorManager = indicatorManager;
    }
    public void StorePosition()
    {
        for (int i = 0; i < nPC1Number + nPC2Number + nPC3Number; i++)
        {
            randomPositions.Enqueue(GetRandomPointWithSpacing());
        }
    }
    Vector3 GetPosition()
    {
        Vector3 posi;
        posi = randomPositions.Dequeue();
        randomPositions.Enqueue(posi);
        return posi;
    }
    public void Spawn(PoolType pT)
    {
        if (pT == PoolType.NPC1)
        {
            Spawn1();
        }
        else if (pT == PoolType.NPC2)
        {
            Spawn2();
        }
        else if (pT == PoolType.NPC3)
        {
            Spawn3();
        }
    }
    public void SpawnWithWaiting(PoolType pT)
    {
        StartCoroutine(WaitAndSpawn(pT));
    }
    public void SpawnPlayerWithWaiting()
    {
        StartCoroutine(WaitSpawnPlayer());
    }
    IEnumerator WaitAndSpawn(PoolType pT)
    {
        yield return new WaitForSeconds(2f);
        Spawn(pT);
    }
    IEnumerator WaitSpawnPlayer()
    {
        yield return new WaitForSeconds(2f);
        SpawnPlayer();
    }
}
