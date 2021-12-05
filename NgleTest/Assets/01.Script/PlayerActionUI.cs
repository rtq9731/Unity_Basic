using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerActionUI : MonoBehaviour
{
    [SerializeField] RectTransform upPanel = null;
    [SerializeField] RectTransform downPanel = null;

    private Vector2 originUpPos = Vector2.zero;
    private Vector2 originDownPos = Vector2.zero;

    private float timer = 0f;

    private void Awake()
    {
        originUpPos = upPanel.anchoredPosition;
        originDownPos = downPanel.anchoredPosition;
    }

    public void DoActionUI()
    {
        timer += Time.deltaTime;

        upPanel.anchoredPosition = new Vector2(0, Mathf.Lerp(0, -128f, timer));
        downPanel.anchoredPosition = new Vector2(0, Mathf.Lerp(0, 128f, timer));
    }

    public void RemoveActionUI()
    {
        DOTween.Complete(upPanel);
        DOTween.Complete(downPanel);

        upPanel.DOAnchorPos(originUpPos, 0.2f);
        downPanel.DOAnchorPos(originDownPos, 0.2f);
        timer = 0f;
    }
}
