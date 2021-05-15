using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyTween : MonoBehaviour
{
    [SerializeField] Ease easeType;
    [SerializeField] CanvasGroup panel;

    void Start()
    {
        float x = transform.position.x;
        transform.DOMoveX(-x, 2f).SetLoops(-1).SetEase(easeType);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DOTween.To(() => { return panel.alpha; }, (value) => panel.alpha = value, 1, 1f);
            transform.DOKill();
        }
    }
}
