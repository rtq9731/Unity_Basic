using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Hit,
        Dead
    }

    public State currentState = State.Idle;
    public float judgeTime = 0.2f;
    public float stunTime = 0.5f;

    private WaitForSeconds ws;
    protected EnemyFOV fov;
    protected EnemyMove move;

    private void Awake()
    {
        ws = new WaitForSeconds(judgeTime);
        fov = GetComponent<EnemyFOV>();
        move = GetComponent<EnemyMove>();
    }

    private void OnEnable()
    {
        StartCoroutine(CheckNextAction());
    }

    IEnumerator CheckNextAction()
    {
        while(true)
        {
            if(GameManager.Player != null)
            {
                CheckState();
                Action();
            }
            yield return ws;
        }
    }

    private void CheckState()
    {
        if (currentState == State.Hit || currentState == State.Dead)
            return;

        bool isTrace = fov.IsTracePlayer();
        bool isView = fov.IsViewPlayer();
        bool isAttack = fov.IsAttackPossible();

        if (isAttack && isView)
        {
            currentState = State.Attack;
        }
        else if (isTrace && isView)
        {
            currentState = State.Chase;
        }
        else
        {
            currentState = State.Patrol;
        }
    }

    protected virtual void Action()
    {
        switch (currentState)
        {
            case State.Idle:
                break;
            case State.Patrol:
                move.SetMove();
                break;
            case State.Chase:
                move.SetChase(GameManager.Player.position);
                break;
            case State.Attack:
                break;
            case State.Hit:
                move.Stop();
                break;
            case State.Dead:
                break;
            default:
                break;
        }
    }

}
