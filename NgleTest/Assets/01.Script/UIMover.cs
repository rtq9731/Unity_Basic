using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIMover : MonoBehaviour
{
    [SerializeField] RectTransform upPanel = null;
    [SerializeField] RectTransform downPanel = null;

    private Vector2 originUpPos = Vector2.zero;
    private Vector2 originDownPos = Vector2.zero;

    float timesleepTime = 0f;

    private void Start()
    {
        originDownPos = downPanel.anchoredPosition;
        originUpPos = upPanel.anchoredPosition;
    }

    public void DoActionUI()
    {
        timesleepTime += Time.deltaTime;

        Debug.Log(Mathf.Lerp(0, -128f, timesleepTime));
        upPanel.anchoredPosition = new Vector2(0, Mathf.Lerp(0, -128f, timesleepTime));
        downPanel.anchoredPosition = new Vector2(0, Mathf.Lerp(0, 128f, timesleepTime));
    }

    public void RemoveActionUI()
    {
        upPanel.DOAnchorPos(originUpPos, 0.2f);
        downPanel.DOAnchorPos(originDownPos, 0.2f);

        timesleepTime = 0f;
        originUpPos = Vector2.zero;
        originDownPos = Vector2.zero;
    }

}
