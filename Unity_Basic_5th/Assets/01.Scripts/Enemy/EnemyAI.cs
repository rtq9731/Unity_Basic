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
        Skill,  //��ų ��� ����
        Stun,  //���ϴ��� ����
        Dead
    }
    public State currentState = State.Idle;
    public float judgeTime = 0.2f;
    public float stunTime = 0.5f;

    public StatusAnimation statusAnim; 

    private WaitForSeconds ws;
    protected EnemyMove move;
    protected EnemyFOV fov;
    protected EnemyAttack attack;

    private void Awake()
    {
        ws = new WaitForSeconds(judgeTime);
        fov = GetComponent<EnemyFOV>();
        move = GetComponent<EnemyMove>();
        attack = GetComponent<EnemyAttack>(); //�̷��� �ϸ� �ڱ����� �´� Attack �� ��������
        statusAnim = GetComponentInChildren<StatusAnimation>();
    }

    private void OnEnable()
    {
        StartCoroutine(CheckNextAction());
    }

    IEnumerator CheckNextAction()
    {
        while (true)
        {
            if(GameManager.Player != null) // ������ �ڵ� 
            {
                // ?? ������ ����üũ , ���µ��� �׼�

                CheckState();

                Action();
            }
            yield return ws;
        }
    }

    protected virtual void CheckState()
    {
        if(currentState == State.Hit || currentState == State.Dead || currentState == State.Stun)
        {
            return;
        }

        bool isTrace = fov.IsTracePlayer();
        bool isView = fov.IsViewPlayer();
        bool isAttack = fov.IsAttackPossible();

        if(isAttack && isView && isTrace)
        {
            currentState = State.Attack;
        }
        else if(isTrace && isView)
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
                if (attack != null)
                    attack.isAttack = false;
                move.SetMove();
                break;
            case State.Chase:
                if (attack != null)
                    attack.isAttack = false;
                move.SetChase(GameManager.Player.position);
                break;
            case State.Attack:
                move.Stop();
                if (attack != null)
                    attack.isAttack = true;
                break;
            case State.Hit:
                move.Stop();
                if (attack != null)
                    attack.isAttack = false;
                break;
            case State.Dead:
                break;           
        }
    }

    public void SetHit()
    {
        currentState = State.Hit;
        move.Stop();
        StartCoroutine(Recover(stunTime));
    }

    public void SetStun(float time = 0)
    {
        currentState = State.Stun;
        if (time == 0)
            time = stunTime;

        if(statusAnim != null)
        {
            statusAnim.PlayeStun(time);
        }

        //���⿡ ���� �ִϸ��̼� ���
        StartCoroutine(Recover(time));
    }

    private IEnumerator Recover( float time)
    {
        yield return new WaitForSeconds(time);

        currentState = State.Patrol;
    }

    public void SetDead()
    {
        currentState = State.Dead;
        if (attack != null)
            attack.isAttack = false;
        move.Stop();
    }
}
     