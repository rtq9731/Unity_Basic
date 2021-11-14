using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public RectTransform tooltipTextTrm;
    public Text tooltipText;

    private CanvasGroup tooltipCG;
    private Vector3 initPosition;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("��� : �ټ��� UI Manager�� �������Դϴ�.");
        }
        
        instance = this;

        tooltipCG = tooltipTextTrm.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        //�ʱ��� �������� �����صΰ�
        initPosition = tooltipTextTrm.localPosition;
    }

    public static void ShowToolTip(string text)
    {
        instance.tooltipText.text = text;

        Sequence seq = DOTween.Sequence();
        CanvasGroup cg = instance.tooltipCG;
        seq.Append(DOTween.To(() => cg.alpha, value => cg.alpha = value, 1, 0.8f));
        float y = instance.initPosition.y;
        seq.Join(instance.tooltipTextTrm.DOLocalMoveY(y + 120f, 0.5f));
    }

    public static void CloseTooltip()
    {
        DOTween.Clear(); //��� Ʈ���� �����Ű�� 
        //�������� ���� ������ �ٽ� �����ϰ� �ٲٰ� initPosition���� ������ ��
        CanvasGroup cg = instance.tooltipCG;
        Sequence seq = DOTween.Sequence();
        seq.Append(DOTween.To(() => cg.alpha, value => cg.alpha = value, 0, 0.8f));
        seq.Join(instance.tooltipTextTrm.DOLocalMoveY(instance.initPosition.y, 0.5f));
    }

}
