using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMove))]
public class EnemyFOV : MonoBehaviour
{
    public float viewRange = 10f;
    [Range(0, 360)]
    public float viewAngle = 40f;
    public float atkRange = 1f;

    public LayerMask obstacleLayer;
    private int playerLayer;

    private EnemyMove enemyMove;

    private void Awake()
    {
        enemyMove = GetComponent<EnemyMove>();
        playerLayer = LayerMask.NameToLayer("PLAYER");
    }

    public Vector2 CirclePoint(float angle)
    {
        if(enemyMove != null)
        {
            angle += enemyMove.GetFront().x < 0 ? -90f : 90f;
        }
        else
        {
            angle += 90;
        }

        return new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public bool IsTracePlayer()
    {
        bool isTrace = false;
        Collider2D col = Physics2D.OverlapCircle(transform.position, viewRange, 1 << playerLayer);

        if(col != null)
        {
            Vector2 dir = GameManager.Player.position - transform.position;

            if(Vector2.Angle(enemyMove.GetFront(), dir) < viewAngle * 0.5f)
            {
                isTrace = true;
            }
        }
        return isTrace;
    }

    public bool IsViewPlayer()
    {
        bool isView = false;
        Vector2 dir = GameManager.Player.position - transform.position;
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, dir.normalized, viewRange, obstacleLayer);
        if(hit2D.collider != null)
        {
            isView = hit2D.collider.gameObject.CompareTag("PLAYER");
        }

        return isView;
    }

    public bool IsAttackPossible()
    {
        return (GameManager.Player.position - transform.position).sqrMagnitude <= atkRange * atkRange;
    }
}
