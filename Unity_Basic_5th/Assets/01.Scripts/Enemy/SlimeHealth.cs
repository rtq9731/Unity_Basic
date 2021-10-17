using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHealth : LivingEntity
{
    public Color hitColor;
    public int coinCount = 3;

    BoxCollider2D boxCollider2D;
    Rigidbody2D rigid;
    SlimeAnimation slimeAnim;
    EnemyAI ai;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        slimeAnim = GetComponent<SlimeAnimation>();
        ai = GetComponent<EnemyAI>();
    }

    private void OnEnable()
    {
        boxCollider2D.enabled = true;
        rigid.gravityScale = 1f;
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void OnDamage(int damage, Vector2 hitPoint, Vector2 normal)
    {
        rigid.velocity = Vector2.zero;
        rigid.AddForce(normal * -damage * 2, ForceMode2D.Impulse);
        slimeAnim.SetHit();
        base.OnDamage(damage, hitPoint, normal);

        ai.SetHit(); // 일시적으로 AI 정지 및 recover 시간이 끝나면 다시 작동함

        // 피격 파티클 재생
        BloodParticle hitParticle = PoolManager.GetItem<BloodParticle>();
        hitParticle.SetParticleColor(hitColor);
        hitParticle.SetRotation(normal);
        hitParticle.Play(hitPoint);
    }

    protected override void OnDie()
    {
        rigid.gravityScale = 0;
        rigid.velocity = Vector2.zero;
        slimeAnim.SetDead();
        boxCollider2D.enabled = false;

        //코인 생성 코드

        Invoke("DeadProcess", 1f);
    }

    private void DeadProcess()
    {
        gameObject.SetActive(false);
    }
}
