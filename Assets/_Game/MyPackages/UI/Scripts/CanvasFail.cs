using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasFail : UICanvas
{
    [SerializeField] GameObject realFail;
    [SerializeField] GameObject fakeFail;
    [SerializeField] TextMeshProUGUI counterText;
    bool waiting=false;
    int count=5;
    int countLimit=5;
    private void OnEnable()
    {
        fakeFail.SetActive(true);
        realFail.SetActive(false);
        count = 5;
        counterText.text = count.ToString();
        waiting = false;
    }
    private void Update()
    {
        if (!waiting)
        {
            waiting = true;
            StartCoroutine(Wait());
        }

    }
    IEnumerator Wait()
    {
        for (int i = 0; i < countLimit; i++)
        {
            yield return new WaitForSeconds(1f);
            count--;
            counterText.text = count.ToString();
        }
        if (count == 0)
        {
            realFail.SetActive(true);
            fakeFail.SetActive(false);
        }
    }
    public void CloseButton()
    {
        fakeFail.SetActive(false);
        realFail.SetActive(true);
    }
    public void ReviveButton()
    {
        UIManager.Instance.CloseUI<CanvasFail>(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }
}
