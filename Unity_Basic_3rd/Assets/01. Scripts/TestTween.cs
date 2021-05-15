using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestTween : MonoBehaviour
{
    [SerializeField] CanvasGroup panel;

    private SpriteRenderer sr;

    private bool isMove = false;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        /*/
        if (Input.GetKeyDown(KeyCode.Space) && !isMove)
        {
            isMove = true;
            //transform.DOMoveX(this.transform.position.x + 5, 1).SetEase(Ease.InBounce).OnComplete( () => { Debug.Log("이동완료"); isMove = false; } );
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(isColored == false)
            {
                isColored = true;
                sr.DOColor(Color.cyan, 3);
            }
            else
            {
                isColored = false;
                sr.DOColor(Color.white, 3);
            }
        }
        //*/

        if (Input.GetMouseButtonDown(0) && !isMove)
        {

            Sequence seq = DOTween.Sequence();

            isMove = true;

            seq.Append(transform.DOShakePosition(2, 0.5f));
            seq.Join(transform.DOShakeScale(2f, 0.8f));
            seq.Join(transform.DOShakeRotation(1f, new Vector3(10,50,0)));
            seq.Append(sr.DOColor(Color.black, 0.5f));
            seq.Append(sr.DOColor(Color.white, 0.8f));
            seq.AppendCallback(() => isMove = false);

            Camera.main.DOShakePosition(2f, 0.9f);


        }

    }

}
