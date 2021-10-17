using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimehealth : LivingEntity
{
    public Color hitcolor;

    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rigid;
    private SlimeAnimation anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<SlimeAnimation>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public override void OnDamage(int damage, Vector2 hitPoint, Vector2 normal)
    {       
        rigid.AddForce(-normal * damage * 2, ForceMode2D.Impulse);
        anim.SetHit();
        base.OnDamage(damage, hitPoint, normal);
    }

    protected override void OnDie()
    {
        rigid.gravityScale = 0;
        rigid.velocity = Vector2.zero;
        anim.SetDead();
        boxCollider2D.enabled = false;
        Invoke("DeadProcess", 1f);
    }

    private void DeadProcess()
    {
        gameObject.SetActive(false);
    }
}
