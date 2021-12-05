using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{   
    public Animator swordAnim; 
    private Animator anim;
    private Rigidbody2D rigid;
    private PlayerMove playerMove;
    
    private readonly int hashXMove = Animator.StringToHash("xMove");
    private readonly int hashYSpeed = Animator.StringToHash("ySpeed");
    private readonly int hashIsGround = Animator.StringToHash("isGround");
    private readonly int hashIsJumping = Animator.StringToHash("isJumping");
    private readonly int hashDoubleJump = Animator.StringToHash("doubleJump");
    private readonly int hashAttack = Animator.StringToHash("attack");
    private readonly int hashIsHit = Animator.StringToHash("isHit");
    private readonly int hashHit = Animator.StringToHash("hit");
    private readonly int hashAttackMode = Animator.StringToHash("attackMode");

    private bool jumping = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMove>();
    }

    void Update()
    {
        //immutable , mutable
        anim.SetFloat(hashXMove, Mathf.Abs(rigid.velocity.x ));
        anim.SetFloat(hashYSpeed, rigid.velocity.y); //y속도 넣기
        anim.SetBool(hashIsGround, playerMove.isGround); //땅바닥 상태
    }

    ///<summary>
    /// 플레이어 점핑 애니메이션 재생 매서드
    ///</summary>
    public void Jump()
    {
        if(jumping){
            anim.SetTrigger(hashDoubleJump);
        }else{
            anim.SetBool(hashIsJumping, true);
        }
        jumping = true;
    }

    ///<summary>
    /// 플레이어 점핑 애니메이션 종료 매서드
    ///</summary>
    public void JumpEnd()
    {
        anim.SetBool(hashIsJumping, false);
        jumping = false;
    }

    public void StartAttack(int mode)
    {
        anim.SetTrigger(hashAttack);
        anim.SetInteger(hashAttackMode, mode);

        //요기에는 검기이펙트 애니메이터 관련 코드 추가해야 함.
        swordAnim.SetTrigger(hashAttack);
        swordAnim.SetInteger(hashAttackMode, mode);
    }

    public void StopAttack()
    {
        anim.SetInteger(hashAttackMode, 0); 
        //검기 이펙트도 종료 코드 넣어야 한다.
        swordAnim.SetInteger(hashAttackMode, 0); 
    }

    public void SetHit(bool value)
    {
        anim.SetBool(hashIsHit, value);
        if (value)
        {
            anim.SetTrigger(hashHit);
        }
    }

    public void SetAnimationSpeed(float ratio)
    {
        anim.speed = Mathf.Clamp(ratio, 0.8f, 2f);
    }
}