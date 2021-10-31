using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack : EnemyAttack
{
    private KnifeScript knife;
    protected GoblinAnimation animation;

    protected override void Awake()
    {
        knife = GetComponentInChildren<KnifeScript>();
        animation = GetComponent<GoblinAnimation>();
    }

    public override void Attack()
    {
        knife.SetAttack(true);
        animation.PlayAttack();
    }

    public void SetAttackDisable()
    {
        knife.SetAttack(false);
    }

    // 플레이어가 고블린한테 몸통박치기하면 때릴 부분
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((1 << collision.gameObject.layer & knife.whatIsEnemy) > 0)
        {
            var damageable = collision.transform.GetComponent<IDamageable>();

            if(damageable != null)
            {
                ContactPoint2D cp2 = collision.contacts[0];
                Vector2 normal = cp2.normal;

                if (Mathf.Abs(normal.x) < 0.1f)
                    normal.x = collision.gameObject.transform.position.x < transform.position.x ? 1 : -1;

                damageable.OnDamage(knife.damage, cp2.point, normal, 5);
            }
        }
    }
}
