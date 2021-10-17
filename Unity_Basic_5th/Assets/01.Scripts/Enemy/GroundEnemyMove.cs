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
        base.SetMove();
    }
}
