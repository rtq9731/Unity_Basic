using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinHealth : LivingEntity
{
    public bool isSuperArmor = false;
    public int coinCount = 0;

    BoxCollider2D boxCollider;
    Rigidbody2D rigid;
    GoblinAnimation animation;
    EnemyAI ai;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        animation = GetComponent<GoblinAnimation>();
        ai = GetComponent<EnemyAI>();
    }

    private void OnEnable()
    {
        boxCollider.enabled = true;
        rigid.gravityScale = 1;
    }

    public override void OnDamage(int damage, Vector2 hitPoint, Vector2 normal, float power)
    {
        if(!isSuperArmor)
        {
            rigid.AddForce(normal * -damage * 2, ForceMode2D.Impulse);
            ai.SetHit();
        }
        // 피격 애니메이션 만들려면 여기 만드셈

        base.OnDamage(damage, hitPoint, normal);
    }

    protected override void OnDie()
    {
        rigid.gravityScale = 0;
        rigid.velocity = Vector2.zero;
        animation.SetDead();
        boxCollider.enabled = false;

        ai.SetDead();

        CoinManager.PopCoin(transform.position, coinCount);

        Invoke("DeadProcess", 1f);
    }

    private void DeadProcess()
    {
        gameObject.SetActive(false);
    }
}
