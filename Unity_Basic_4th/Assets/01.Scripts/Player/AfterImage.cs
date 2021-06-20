using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AfterImage : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void SetSprite(Sprite sprite, bool flip, Vector3 pos)
    {
        transform.position = pos;
        spriteRenderer.flipX = flip;
        spriteRenderer.color = Color.white;
        spriteRenderer.sprite = sprite;
        spriteRenderer.DOFade(0, 0.7f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

}
