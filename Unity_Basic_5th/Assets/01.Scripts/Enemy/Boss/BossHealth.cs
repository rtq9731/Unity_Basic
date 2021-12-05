using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : LivingEntity
{
    public Item dropItem;
    public LootBox lootBoxPrefab;

    public int dropCoinCount;

    public int dialogCount = 0;

    private GoblinAnimation _goblinAnim;

    private void Awake()
    {
        _goblinAnim = GetComponent<GoblinAnimation>();
    }

    public override void OnDamage(int damage, Vector2 hitPoint, Vector2 normal, float power = 0)
    {
        base.OnDamage(damage, hitPoint, normal, power);
        //���� HP�ٿ� ��������� �Ѵ�.
        UIManager.SetBossHPBar( (float)currentHP / (float)maxHP);
    }

    protected override void OnDie()
    {
        _goblinAnim.SetDead();

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
        //�������� ����� �̷�������� �Ѵ�.
        CoinManager.PopCoin(transform.position, dropCoinCount);

        if(dropItem != null)
        {
            LootBox lb = Instantiate(lootBoxPrefab, transform.position, Quaternion.identity);
            lb.SetLootItem(dropItem);
            lb.Popup(transform.position);

            //���⿡ ������ HP�ٸ� �����ִ� �� ���ָ� �ȴ�.

            UIManager.HideBossHPBar();

            gameObject.SetActive(false);
        }
    }
}
