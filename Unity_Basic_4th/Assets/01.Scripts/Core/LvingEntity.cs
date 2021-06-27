using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LvingEntity : MonoBehaviour, IDamageable
{
    [SerializeField] int MaxHP;
    protected int currentHP;

    protected virtual void Start()
    {
        currentHP = MaxHP;
    }

    public virtual void OnDamage(int damage, Vector2 hitPoint, Vector2 normal)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            OnDie();
        }
    }

    protected abstract void OnDie();
}
