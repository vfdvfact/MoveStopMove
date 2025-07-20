using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCIndicatorManager : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Camera mainCamera;
    public Canvas canvas;

    private Dictionary<Transform, NPCIndicatorArrow> npcArrowMap = new Dictionary<Transform, NPCIndicatorArrow>();

    public void AddNPC(Transform npc)
    {
        if (!npcArrowMap.ContainsKey(npc))
        {
            NPCIndicatorArrow arrow = Pools.Instance.Spawn<NPCIndicatorArrow>(PoolType.Indicator, Vector3.zero, Quaternion.identity);

            arrow.npcTarget = npc;
            arrow.mainCam = mainCamera;
            arrow.arrowUI = arrow.gameObject.GetComponent<RectTransform>();
            arrow.arrowImage = arrow.gameObject.GetComponent<Image>();
            npcArrowMap[npc] = arrow;
        }
        else
        {
            npcArrowMap[npc].gameObject.SetActive(true);
        }
    }

    public void RemoveNPC(Transform npc)
    {
        if (!npcArrowMap.ContainsKey(npc)) return;
        npcArrowMap[npc].gameObject.SetActive(false);
    }

    public void ClearAll()
    {
        foreach (var arrow in npcArrowMap.Values)
        {
            Destroy(arrow);
        }
        npcArrowMap.Clear();
    }
}
