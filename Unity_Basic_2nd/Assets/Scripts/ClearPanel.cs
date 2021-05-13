using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClearPanel : MonoBehaviour
{
    [SerializeField] GameObject clearPanel;

    void OnEnable()
    {
        clearPanel.GetComponent<RectTransform>().DOAnchorPosY(0, 1);
    }

}
