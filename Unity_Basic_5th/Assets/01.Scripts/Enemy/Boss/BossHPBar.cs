using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossHPBar : MonoBehaviour
{
    public RectTransform fillImage = null;
    public Text fillText = null;

    private Tween tween = null;

    public void SetHP(float value)
    {
        if(tween != null && tween.IsActive())
        {
            tween.Kill();
        }

        value = Mathf.Clamp(value, 0, 1);
        tween = fillImage.DOScaleX(value, 0.2f);
         

        fillText.text = $"{(value * 100).ToString("F2")}%";
    }
}
