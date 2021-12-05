using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator myAnim = null;
    AfterImageScript afterImageScript = null;


    int[] hashAttacks = new int[] { Animator.StringToHash("Attack1"), Animator.StringToHash("Attack2"), Animator.StringToHash("Attack3") };

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        afterImageScript = GetComponent<AfterImageScript>();
    }

    public void OffAfterEffect()
    {
        afterImageScript.isOnAfterEffect = false;
    }

    public void OnAfterEffect()
    {
        afterImageScript.isOnAfterEffect = true;
    }

    public void PlayAttackAnim()
    {
        myAnim.SetTrigger(hashAttacks[Random.Range(0, hashAttacks.Length)]);
    }
}
