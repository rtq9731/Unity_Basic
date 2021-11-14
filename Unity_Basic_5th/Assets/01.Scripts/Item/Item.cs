using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IUseAble
{
    Rigidbody2D rigid;

    public int value;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void PopUp(Vector3 pos)
    {
        transform.position = pos;
        float angle = Random.Range(-45f, 45f);
        Vector2 dir = new Vector2(
                        Mathf.Sin(angle * Mathf.Deg2Rad),
                        Mathf.Cos(angle * Mathf.Deg2Rad));
        rigid.AddForce(dir * 10, ForceMode2D.Impulse);
    }

    public virtual void Use(GameObject target)
    {
        gameObject.SetActive(false);
    }
}
