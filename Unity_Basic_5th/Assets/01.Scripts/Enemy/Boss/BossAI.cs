using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : EnemyAI
{
    private void Start()
    {
        currentState = State.Idle;
        move.Stop();
    }

    //������ ������ �����ϴ� ��
    public void StartBoss()
    {
        currentState = State.Chase;
        //enemyAttack �� Ȱ��ȭ���Ѽ� ���ݽ����ؾ���.
        attack.OnStartAttack();
    }

    protected override void CheckState()
    {
        if (currentState == State.Hit || currentState == State.Dead || currentState == State.Idle
            || currentState == State.Skill || currentState == State.Stun) return;

        bool isAttack = fov.IsAttackPossible();
        bool isTrace = fov.IsTracePlayer();
        
        if(isAttack && isTrace)
        {
            currentState = State.Attack;
        }
        else
        {
            currentState = State.Chase;
        }
    }

    protected override void Action()
    {
        switch(currentState )
        {
            case State.Chase:
                //���� ��Ȱ��ȭ
                if (attack != null) attack.isAttack = false;
                move.SetChase(GameManager.Player.position);
                break;
            case State.Attack:
                move.Stop();
                //����Ȱ��ȭ
                if (attack != null) attack.isAttack = true;
                break;
        }
    }
}
