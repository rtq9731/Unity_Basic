using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : LivingEntity
{
    public Item dropItem;
    public LootBox chestPrefab;

    public int dropCointCount;

    public int dialogCount = 0;

    private GoblinAnimation goblinAnimation;

    private void Awake()
    {
        goblinAnimation = GetComponent<GoblinAnimation>();
    }

    public override void OnDamage(int damage, Vector2 hitPoint, Vector2 normal, float power = 0)
    {
        base.OnDamage(damage, hitPoint, normal, power);
        UIManager.SetBossHPBar((float)currentHP / maxHP);
    }

    protected override void OnDie()
    {
        goblinAnimation.SetDead();

        if(dialogCount > 0)
        {
            GameManager.ShowDialog(dialogCount, () =>
            {
                DropAndDeadProcess();
            });
        }
        else
        {
            DropAndDeadProcess();
        }
    }

    private void DropAndDeadProcess()
    {
        CoinManager.PopCoin(transform.position, dropCointCount);
        UIManager.HideBossHPBar();

        if(dropItem != null)
        {
            LootBox lb = Instantiate(chestPrefab, transform.position, Quaternion.identity);
            lb.SetLoot(dropItem);
            lb.PopUP(transform.position);

            gameObject.SetActive(false);
        }
    }
}
