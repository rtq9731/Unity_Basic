using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private SpriteRenderer sr;
    private PlayerInput playerInput;
    private Rigidbody2D rigid;
    private PlayerAnimation playerAnimation;

    [Header("플레이어 무빙 관련")]
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpPower = 2f;
    [SerializeField] int maxJumpCount = 2;

    float xMove = 0f;

    int jumpCount = 0;
    [Header("플레이어 점프 관련")]
    [SerializeField] MyGizmos groundChecker;
    [SerializeField] LayerMask whatIsGround;

    private bool isJump = false;
    public bool isGround { get; private set; } = true;

    [Header("플레이어 대쉬 관련")]
    [Header("PlayerSpeed에 곱해줄 값이에요.")] [SerializeField] float dashPower = 10f; // 곱셈값
    [SerializeField] float dashTime = 0.2f;
    [SerializeField] int dashCount = 2;

    bool isDash = false;

    [Header("플레이어 대쉬 이펙트 관련")]
    [SerializeField] GameObject afterImagePrefab;
    [SerializeField] Transform afterImageTr = null;

    private void Awake()
    {
        // 대쉬 한번만
        // 땅에 닿으면 대쉬 횟수 초기화

        playerAnimation = gameObject.GetComponent<PlayerAnimation>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        playerInput = gameObject.GetComponent<PlayerInput>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        PoolManager.CreatePool<AfterImage>(afterImagePrefab, afterImageTr, 20);
    }

    private void Update()
    {
        xMove = playerInput.xMove;

        if (xMove >= 0) { sr.flipX = false; }
        else { sr.flipX = true; }

        if (playerInput.isJump && (isGround || jumpCount <= maxJumpCount) ) { isJump = true; }

        if(!isJump)
        {
            if(Physics2D.OverlapCircle(groundChecker.transform.position, groundChecker.radius, whatIsGround))
            {
                isGround = true;
                dashCount = 2;
                
                jumpCount = 0;
            }
        }

        if(playerInput.isDash && !isDash && dashCount >= 0)
        {
            isDash = true;
            StartCoroutine(Dash());
            dashCount--;
        }
    }

    private void FixedUpdate()
    {
        if (isDash) return;
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

        if (isGround && rigid.velocity.y < 0.1f)
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

    IEnumerator Dash()
    {
        Vector2 dir = sr.flipX ? transform.right * -1 : transform.right;
        Debug.Log(dir);
        rigid.velocity = Vector2.zero;
        rigid.AddForce(dir * dashPower, ForceMode2D.Impulse);
        rigid.gravityScale = 0;

        float time = 0;
        float afterTime = 0;
        float targetTime = Random.Range(0.01f, 0.07f);
        while (isDash)
        {
            time += Time.deltaTime;
            afterTime += Time.deltaTime;

            if(afterTime >= targetTime)
            {
                AfterImage ai = PoolManager.GetItem<AfterImage>();
                ai.SetSprite(sr.sprite, sr.flipX, transform.position);
                targetTime = Random.Range(0.01f, 0.07f);
                afterTime = 0f;
            }

            if (time >= dashTime)
            {
                isDash = false;
            }

            yield return null;
        }

        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 1;
    }

}
