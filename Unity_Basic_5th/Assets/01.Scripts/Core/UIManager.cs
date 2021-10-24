using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    public RectTransform tooltipTextTrm;
    public Text tooltipText;

    private CanvasGroup tooltipCG;
    private Vector3 initPosition;

    Sequence seq = null;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("경고 : 다수의 UI Manager가 실행중입니다.");
        }

        instance = this;

        tooltipCG = tooltipTextTrm.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        instance.initPosition = instance.tooltipTextTrm.localPosition;
    }

    public static void ShowToolTip(string text)
    {
        if(instance.seq != null)
        instance.seq.Kill();

        instance.tooltipText.text = text;
        instance.seq = DOTween.Sequence();

        CanvasGroup cg = instance.tooltipCG;

        instance.seq.Append(DOTween.To(() => cg.alpha, vaule => cg.alpha = vaule, 1, 0.8f));
        float y = instance.initPosition.y;
        instance.seq.Join(instance.tooltipTextTrm.DOLocalMoveY(y + 110f, 0.5f));
    }

    public static void CloseToolTip()
    {
        instance.seq.Kill();

        CanvasGroup cg = instance.tooltipCG;

        instance.seq = DOTween.Sequence();

        instance.seq.Append(DOTween.To(() => cg.alpha, vaule => cg.alpha = vaule, 0, 0.8f));
        instance.seq.Join(instance.tooltipTextTrm.DOLocalMoveY(instance.initPosition.y, 0.5f));
    }
}
