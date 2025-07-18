using UnityEngine;
using UnityEngine.UI;

public class NPCIndicatorArrow : GameUnit
{
    public Transform npcTarget;
    public Camera mainCam;
    public RectTransform arrowUI;

    public float screenEdgeOffset = 50f;

    RectTransform canvasRect;

    void Start()
    {
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 screenPos = mainCam.WorldToScreenPoint(npcTarget.position);
        bool isOffScreen = screenPos.z < 0 ||
                           screenPos.x < 0 || screenPos.x > Screen.width ||
                           screenPos.y < 0 || screenPos.y > Screen.height;

        gameObject.SetActive(isOffScreen);

        if (isOffScreen)
        {
            Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;
            Vector3 dir = (screenPos - screenCenter).normalized;

            Vector3 clampedScreenPos = screenCenter + dir * (Mathf.Min(screenCenter.x, screenCenter.y) - screenEdgeOffset);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, clampedScreenPos, null, out Vector2 canvasPos);
            arrowUI.localPosition = canvasPos;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrowUI.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}
