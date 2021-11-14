using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBossAttack : GoblinAttack
{
    public float farDistance = 5f; //�̰� �̻� �־����� �����۴´�.
    public float dashAttackDelay = 8f; //�밡�� �۴� ������ ������ �ð�
    private float lastDashAttackTime = 0; //���������� �밡�� �۾Ҵ� �ð�

    //�ؽ�Ʈ�� ��µ� �� �ִ� �� ������ �־�� ��
    public ShortText shortText; 

    private EnemyAI ai;
    public bool isPattern = false; //Ư������ ���������� �� �� �ִ� ����
    private BossMove move;  //������ ���� ��ũ��Ʈ

    public LayerMask whatIsWall; // ���� ������ �˾ƾ� �����ڰ� �����Ѵ�.

    protected override void Awake()
    {
        base.Awake();
        ai = GetComponent<EnemyAI>();
        move = GetComponent<BossMove>();
    }

    public override void OnStartAttack()
    {
        lastDashAttackTime = Time.time; //�����ϸ� ��þ��� �ð� �ʱ�ȭ
    }

    protected override void Update()
    {
        if (ai.currentState == EnemyAI.State.Idle) return; //�����´� �ƹ��͵� ����

        Vector2 distanceVector = GameManager.Player.position - transform.position;
        if(!isPattern && distanceVector.sqrMagnitude >= farDistance * farDistance 
            && lastDashAttackTime + dashAttackDelay <= Time.time)
        {
            isPattern = true;
            ai.currentState = EnemyAI.State.Skill;
            move.Stop();
            StartCoroutine(DashSequence());
        }else if(!isPattern)
        {
            base.Update(); //������ �ְ� ���ϼ��� �ƴϸ� ǫ��ǫ��! 
        }
    }
    //���� ������
    IEnumerator DashSequence()
    {
        //����ġ�� ����� �ؽ�Ʈ�� ����ϰ�
        shortText.ShowText("����ġ�� ����!", 1f);
        //���⼭ �÷��̾��� ������ Ȯ���ϰ� ����
        Vector2 dir = new Vector2(GameManager.Player.position.x - transform.position.x, 0);
        yield return new WaitForSeconds(1.2f);

        move.SetForce(dir.normalized * 20);

        yield return new WaitForSeconds(2f);
        // ���� ���� �����ھƼ� ������ �ɷȰų�, �Ǵ� �÷��̾ �ھƼ� �������� ��ų�
        if(ai.currentState != EnemyAI.State.Stun)
        {
            ai.currentState = EnemyAI.State.Chase; //���ϰɸ��� �ʾҴٸ� �������·� ����
        }
        lastDashAttackTime = Time.time;
        isPattern = false;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isPattern)
        {
            base.OnCollisionEnter2D(collision);
        }
        else
        {
            if( (1 << collision.gameObject.layer & knife.whatIsEnemy) > 0  )
            {
                LivingEntity le = collision.gameObject.GetComponent<LivingEntity>();
                ContactPoint2D cp2d = collision.contacts[0];
                Vector2 cpNormal = cp2d.normal;

                if(Mathf.Abs(cpNormal.x) < 0.1f)
                {
                    cpNormal.x = collision.gameObject.transform.position.x < transform.position.x ? 1 : -1;
                }
                if (le != null)
                    le.OnDamage(knife.damage * 3, cp2d.point, cpNormal, 8);
            }
            else if( (1 << collision.gameObject.layer & whatIsWall ) > 0)
            {
                ai.SetStun(); //���� �ε����� ����ó��
            }
        }
        
    }
}
