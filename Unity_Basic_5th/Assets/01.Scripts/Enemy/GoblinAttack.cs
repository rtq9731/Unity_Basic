using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack : EnemyAttack
{
    protected KnifeScript knife;
    protected GoblinAnimation anim;

    protected override void Awake()
    {
        knife = GetComponentInChildren<KnifeScript>();
        anim = GetComponent<GoblinAnimation>();
    }

    public override void Attack()
    {
        knife.SetAttack(true);
        anim.PlayAttack();
    }

    public void SetAttackDisable()
    {
        knife.SetAttack(false);
    }

    //�÷��̾ ����� �����ġ�� �� ��� . �¾ƾ���
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if( (1 << collision.gameObject.layer & knife.whatIsEnemy) > 0)
        {
            //�浹�� �༮���Լ� IDamageable�� �ִ��� üũ�ؼ�
            IDamageable hp = collision.gameObject.GetComponent<IDamageable>();

            //�׳༮�� null�� �ƴϸ� 
            if (hp != null)
            {
                ContactPoint2D cp2 = collision.contacts[0];
                Vector2 normal = cp2.normal;
                if (Mathf.Abs(normal.x) < 0.1f)
                    normal.x = collision.gameObject.transform.position.x < transform.position.x ? 1 : -1;
                
                hp.OnDamage(knife.damage, cp2.point, normal, 5);
            }
            // �浹�� �༮�� contacts[0]�� �����ͼ� point��, normal�� �̾Ƴ���
            //�װɷ� �������� ���Ѵ�.
        }
    }
}
