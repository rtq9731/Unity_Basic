using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : Interactable
{
    public Item dropItem;

    private readonly int hashOpenTrigger = Animator.StringToHash("open");
    private BoxCollider2D boxCol;
    private Animator anim;
    private Rigidbody2D rigid;

    private bool canUse = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        canUse = false;
    }

    private void Start()
    {
        PopUP(transform.position);
    }

    public void SetLoot(Item item)
    {
        dropItem = item;
    }

    public override void Use(GameObject target)
    {
        if (!canUse || used)
        {
            return;
        }

        used = true;
        anim.SetTrigger(hashOpenTrigger);

        Item item = Instantiate(dropItem, transform.position, Quaternion.identity);
        item.PopUp(transform.position);
    }

    public void PopUP(Vector3 pos)
    {
        transform.position = pos;
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        StartCoroutine(OpenDelay());
    }

    private IEnumerator OpenDelay()
    {
        yield return new WaitForSeconds(1f);
        rigid.gravityScale = 0;
        boxCol.isTrigger = true;
        rigid.velocity = Vector2.zero;
        canUse = true;
    }
}
