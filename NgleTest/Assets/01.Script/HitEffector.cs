using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HitEffector : MonoBehaviour
{
    [SerializeField] GameObject mainVcam = null;

    public void HitEffect(Vector2 pos)
    {
        pos.x += 1f;
        transform.position = pos;

        Vector2 camOriginPos = mainVcam.transform.position;
        mainVcam.transform.DOShakePosition(0.1f).OnComplete(() => mainVcam.transform.position = camOriginPos);
    }
}
