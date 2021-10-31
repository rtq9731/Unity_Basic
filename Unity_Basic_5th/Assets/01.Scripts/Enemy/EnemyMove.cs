using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyMove : MonoBehaviour
{
    protected SpriteRenderer sr;
    public bool facingRight = true; // 오른쪽을 보고 있는가?

    public float judgeDistance = 0.1f;
    public float currentSpeed;

    public float chaseSpeed = 2;
    public float moveSpeed = 1f;

    protected bool isChase = false;

    protected Vector2 destination;
    protected bool moveSet = false;
    protected Rigidbody2D rigid;

    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    public Vector2 GetFront()
    {
        if (sr == null)
            return transform.right;

        if(facingRight)
        {
            return transform.localScale.x > 0 ? transform.right * -1 : transform.right;
        }
        else
        {
            return transform.localScale.x > 0 ? transform.right : transform.right * -1;
        }
    }

    public virtual void Stop()
    {
        rigid.velocity = Vector2.zero;
        moveSet = false;
    }

    public virtual void SetChase(Vector2 target)
    {
        destination = target;
        currentSpeed = chaseSpeed;
        isChase = true;
        moveSet = true;
    }

    public virtual void SetMove()
    {
        isChase = false;
        moveSet = true;
        currentSpeed = moveSpeed;
    }

    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
