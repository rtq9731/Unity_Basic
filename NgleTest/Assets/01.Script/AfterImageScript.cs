using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AfterImageScript : MonoBehaviour
{
    [SerializeField] GameObject afterImage = null;
    [SerializeField] Transform afterImageParent = null;
    [SerializeField] private float afterEffectInterval = 1f;
    [SerializeField] private float afterImageLifeTime = 1f;

    Queue<GameObject> afterEffectQueue = new Queue<GameObject>();

    private float lastAfterEffectTime = 0f;

    public bool isOnAfterEffect = false;

    private void Update()
    {
        if(isOnAfterEffect)
        {
            if(lastAfterEffectTime + afterEffectInterval <= Time.time)
            {
                MakeAfterEffect();
                lastAfterEffectTime = Time.time;
            }
        }
    }

    private void MakeAfterEffect()
    {
        lastAfterEffectTime = Time.time;
        GameObject curAfterIamge = Instantiate(afterImage, afterImageParent);
        SpriteRenderer curAfterIamgeSR = curAfterIamge.GetComponent<SpriteRenderer>();
        curAfterIamgeSR.sprite = GetComponent<SpriteRenderer>().sprite;
        curAfterIamgeSR.DOFade(0, afterImageLifeTime).OnComplete(() => curAfterIamge.SetActive(false));
        curAfterIamge.transform.position = transform.position;
        afterEffectQueue.Enqueue(curAfterIamge);
    }

    private void RemoveAfterEffect()
    {
        while(afterEffectQueue.Count >= 1)
        {
            afterEffectQueue.Dequeue().SetActive(false);
        }
    }
}
