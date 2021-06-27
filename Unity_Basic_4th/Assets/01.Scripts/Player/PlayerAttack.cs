using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("공격관련")]
    [SerializeField] private float timeBetAttack = 0.8f;
    [SerializeField] private float lastAttackTime;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int attackDamage = 2;
    [SerializeField] private LayerMask whatIsEnemy;

    private PlayerInput playerInput;
    private PlayerAnimation playerAnimation;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimation = GetComponent<PlayerAnimation>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        lastAttackTime = Time.time;
    }

    private void Update()
    {
        if(playerInput.isAttack && Time.time >= lastAttackTime + timeBetAttack)
        {
            lastAttackTime = Time.time;
            Attack();
        }
    }

    private void Attack()
    {
        playerAnimation.StartAttack();

        Vector2 dir = spriteRenderer.flipX ? transform.right * -1 : transform.right;

        Debug.DrawRay(transform.position, dir * attackRange, Color.red, 0.5f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, attackRange, whatIsEnemy);
        if (hit.collider) // hit의 컬라이더를 뱉음.
        {
            IDamageable iDamage = hit.collider.GetComponent<IDamageable>();

            if(iDamage != null)
            {
                iDamage.OnDamage(attackDamage, hit.point, hit.normal);
            }
        }


    }
}
