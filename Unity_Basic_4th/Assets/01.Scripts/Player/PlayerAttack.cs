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
    }
}
