using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;

    [SerializeField] Transform cupObj;

    private SpriteRenderer spriteRenderer;

    private Vector3 originPos;
    private float originX;
    private Items items;

    private void Awake()
    {
        originPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Reset()
    {
        transform.position = originPos;
        cupObj.position = transform.position;
        gameObject.SetActive(false);
    }

    public void SetItem(Items items)
    {
        this.items = items;
        cupObj.gameObject.SetActive(false);
        spriteRenderer.sprite = sprites[(int)items];
    }

    public void DropItem(float originX)
    {
        this.originX = originX;

        transform.DOMoveY(-2f, 1f).OnComplete(() =>
        {
            GameManager.DropComplete();
        });
    }

    public void CupAndShake(List<int> shuffleSeq)
    {
        cupObj.Translate(new Vector3(0, 3f, 0));
        cupObj.gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(cupObj.DOMoveY(transform.position.y - 0.3f, 1f));
        sequence.Append(cupObj.DOShakePosition(1f, 0.3f));

        for (int i = 0; i < shuffleSeq.Count; i++)
        {
            sequence.Append(transform.DOMoveX(originX + shuffleSeq[i] * 3, 0.5f));
        }
        sequence.AppendCallback(() =>
        {
            GameManager.ShuffleComplete();
        });

    }

    public void Open()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(cupObj.DOShakePosition(1f, 0.3f));
        sequence.Append(cupObj.DOMoveY(cupObj.position.y + 2f, 0.8f));
        sequence.AppendCallback(() => 
        {
            cupObj.gameObject.SetActive(false);
            GameManager.GiveItemToCat(items);
        });
    }

}
