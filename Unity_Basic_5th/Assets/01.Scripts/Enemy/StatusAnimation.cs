using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusAnimation : MonoBehaviour
{
    private int hashStunBool = Animator.StringToHash("stun");
    private int hashExclaimTrigger = Animator.StringToHash("exclaim");
    private int hashQuestionTrigger = Animator.StringToHash("question");

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayeStun(float stunTime)
    {
        StartCoroutine(StunRecover(stunTime));
    }

    IEnumerator StunRecover(float time)
    {
        anim.SetBool(hashStunBool, true);

        yield return new WaitForSeconds(time - 0.2f);

        anim.SetBool(hashStunBool, false);
    }

    public void PlayExclaim()
    {
        anim.SetTrigger(hashExclaimTrigger);
    }

    public void PlayQuestion()
    {
        anim.SetTrigger(hashQuestionTrigger);
    }
}
