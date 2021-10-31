using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    public float damageDelay = 1f;
    public float recoverDelay = 0.5f;

    private PlayerMove playerMove;
    private float lastDamageTime;

    private void Awake()
    {
        lastDamageTime = Time.time;
        playerMove = GetComponent<PlayerMove>();
    }

    public override void OnDamage(int damage, Vector2 hitPoint, Vector2 normal, float power)
    {
        if (lastDamageTime + damageDelay > Time.time) return;

        lastDamageTime = Time.time;

        base.OnDamage(damage, hitPoint, normal, power);

        playerMove.SetHit(normal, power, recoverDelay);
    }

    protected override void OnDie()
    {
        throw new System.NotImplementedException();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Item item = collision.transform.GetComponent<Item>();
        if(item != null)
        {
            item.Use(this.gameObject);
        }
    }
}
