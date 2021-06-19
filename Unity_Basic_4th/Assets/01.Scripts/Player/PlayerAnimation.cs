using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator = null;
    private Rigidbody2D rigid = null;
    private PlayerMove playerMove = null;

    //string to hash -> 해쉬값을 받아옴.
    private readonly int hashXMove = Animator.StringToHash("xMove");
    private readonly int hashYSpeed = Animator.StringToHash("ySpeed");
    private readonly int hashIsGround = Animator.StringToHash("isGround");
    private readonly int hashIsJumping = Animator.StringToHash("isJumping");
    private readonly int hashDoubleJump = Animator.StringToHash("doubleJump");

    private bool bJumping = false;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        playerMove = gameObject.GetComponent<PlayerMove>();
    }

    void Update()
    {
        //immutable ( 불변 ), mutable ( 가변 )
        //string은 immutable 이라 바뀌지 않음. 때문에 값을 바꿀때마다 파괴 / 생성을 반복함
        //최적화를 위해서 해쉬를 받아오도록 변경
        animator.SetFloat(hashXMove, Mathf.Abs(rigid.velocity.x));
        animator.SetFloat(hashYSpeed, rigid.velocity.y);
        animator.SetBool(hashIsGround, playerMove.isGround);
    }

    public void animJump()
    {
        if(bJumping)
        {
            animator.SetTrigger(hashDoubleJump);
        }
        else
        {
            animator.SetBool(hashIsJumping, true);
        }
        bJumping = true;
    }

    public void animJumpEnd()
    {
        animator.SetBool(hashIsJumping, false);
        bJumping = false;
    }
}

// 프로그램계에서 가장 오래된 난제 -> 정렬과 탐색
// 이걸 효과적으로 수행하기 위해 스택, 큐, 이진 트리, B+Tree 등 자료구조를 만들어냄
// 3, 5, 22 , 46, 156, 200 -> 비용 최소화를 위해 이진트리를 만듬 ( 로그 2의 n의 시간복잡도를 만듬 )
// -> 하지만 부족했던 프로그래머들

// 해쉬 테이블을 만들자 해쉬 함수 발싸 히히
// 해쉬함수 -> 들어가면히히수학히히라서 설명은 생략하지만 대충 해쉬로 바꿔줌