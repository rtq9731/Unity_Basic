using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimation : MonoBehaviour
{
    private Animator animator;
    private readonly int hashIsDead = Animator.StringToHash("IsDead");
    private readonly int hashDie = Animator.StringToHash("Die");
    private readonly int hashHit = Animator.StringToHash("Hit");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetDead()
    {
        animator.SetBool(hashIsDead, true);
        animator.SetTrigger(hashDie);
    }

    public void SetHit()
    {
        animator.SetTrigger(hashHit);
    }

}
