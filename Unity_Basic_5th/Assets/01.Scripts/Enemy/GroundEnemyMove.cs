using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyMove : EnemyMove
{
    public LayerMask whatIsGround;
    private Vector2 moveDir;

    private void Start()
    {
        moveDir = transform.right;
    }

    public override void SetMove()
    {
        if(isChase)
        {
            moveDir = transform.right;
        }

        base.SetMove();
    }

    private void FixedUpdate()
    {
        if(moveSet)
        {
            if(isChase)
            {
                moveDir = destination - (Vector2)transform.position;
            }

            rigid.velocity = new Vector2(moveDir.x * currentSpeed * GameManager.TimeScale, rigid.velocity.y);

            if(facingRight)
            {
                if(moveDir.x > 0 && sr.flipX || moveDir.x < 0 && !sr.flipX)
                {
                    Flip();
                }
            }
            else
            {
                if(moveDir.x < 0 && sr.flipX || moveDir.x > 0 && !sr.flipX)
                {
                    Flip();
                }
            }

            if(!CheckGround())
            {
                if(isChase)
                {
                    Stop();
                }
                else
                {
                    moveDir *= -1;
                }
            }
        }
    }

    private bool CheckGround()
    {
        Vector2 pos = transform.position;
        Vector2 frontPosition = new Vector2(pos.x + moveDir.x * 0.35f, pos.y - 0.5f);

        return Physics2D.OverlapCircle(frontPosition, 0.2f, whatIsGround);
    }
}
