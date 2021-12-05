using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float attackRange = 1f;
    [SerializeField] LayerMask whatIsEnemy;

    public void Attack(HitEffector hitEffector)
    {
        Debug.DrawRay(transform.position, transform.right, Color.cyan, 10);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, attackRange, whatIsEnemy);

        if(hit)
        {
            hit.transform.GetComponent<Attackable>().GetAttack(hitEffector);
        }
    }
}
