using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator = null;
    private Rigidbody2D rigid = null;
    private PlayerMove playerMove = null;

    //string to hash -> �ؽ����� �޾ƿ�.
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
        //immutable ( �Һ� ), mutable ( ���� )
        //string�� immutable �̶� �ٲ��� ����. ������ ���� �ٲܶ����� �ı� / ������ �ݺ���
        //����ȭ�� ���ؼ� �ؽ��� �޾ƿ����� ����
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

// ���α׷��迡�� ���� ������ ���� -> ���İ� Ž��
// �̰� ȿ�������� �����ϱ� ���� ����, ť, ���� Ʈ��, B+Tree �� �ڷᱸ���� ����
// 3, 5, 22 , 46, 156, 200 -> ��� �ּ�ȭ�� ���� ����Ʈ���� ���� ( �α� 2�� n�� �ð����⵵�� ���� )
// -> ������ �����ߴ� ���α׷��ӵ�

// �ؽ� ���̺��� ������ �ؽ� �Լ� �߽� ����
// �ؽ��Լ� -> ������������������ ������ ���������� ���� �ؽ��� �ٲ���