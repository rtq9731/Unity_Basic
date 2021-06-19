using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("플레이어 무빙 관련")]
    [SerializeField] float speed = 1f;
    [SerializeField] float dashSpeed = 2f; // 곱셈값
    [SerializeField] float jumpPower = 2f;
    [SerializeField] int maxJumpCount = 2;

    float xMove = 0f;

    int jumpCount = 0;

    [Header("플레이어 점프 관련")]
    [SerializeField] MyGizmos groundChecker;
    [SerializeField] LayerMask whatIsGround;

    private bool isJump = false;
    public bool isGround { get; private set; } = true;

    private SpriteRenderer sr;
    private PlayerInput playerInput;
    private Rigidbody2D rigid;
    private PlayerAnimation playerAnimation;


    private void Awake()
    {
        playerAnimation = gameObject.GetComponent<PlayerAnimation>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        playerInput = gameObject.GetComponent<PlayerInput>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        if (playerInput.isDash) { xMove = playerInput.xMove * dashSpeed; }
        else { xMove = playerInput.xMove; }

        if (xMove >= 0) { sr.flipX = false; }
        else { sr.flipX = true; }

        if (playerInput.isJump && (isGround || jumpCount <= maxJumpCount) ) { isJump = true; }

        if(!isJump)
        {
            if(Physics2D.OverlapCircle(groundChecker.transform.position, groundChecker.radius, whatIsGround))
            {
                isGround = true;
                jumpCount = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2( xMove * speed, rigid.velocity.y);
        if (isJump)
        {
            if(jumpCount >= maxJumpCount && jumpCount >= 1)
            {
                isJump = false;
                isGround = false;
                return;
            }
            else if (jumpCount >= 1)
            {
                rigid.velocity = Vector2.zero;
                jump();
                return;
            }

            jump();
            isJump = false;

        }

        if (isGround = true && rigid.velocity.y < 0.1f)
        {
            playerAnimation.animJumpEnd();
        }
    }

    void jump()
    {
        jumpCount++;
        rigid.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        isGround = false;
        playerAnimation.animJump();
    }

}
