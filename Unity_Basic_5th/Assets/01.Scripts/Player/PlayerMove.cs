using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float jumpForce = 30f;
    public int jumpCount = 1;
    private int currentJumpCount;

    [Header("바닥 감지 관련")]
    public bool isGround;
    public Transform groundChecker;
    public LayerMask whatIsGround;

    private Rigidbody2D rigid;
    private PlayerInput input;
    private SpriteRenderer spriteRenderer;

    private bool isJump = false;
    private PlayerAnimation playerAnimation;

    [Header("대시 관련")]
    public GameObject afterImagePrefab;
    public Transform afterImageTrm;
    public bool canDash = false;
    public float dashPower = 10f;
    public float dashTime = 0.2f;
    public float dashCoolTime = 5f;

    private float currentDashCooltime = 0f;
    private bool isHit = false;
    private bool isDash = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimation = GetComponent<PlayerAnimation>();

        PoolManager.CreatePool<AfterImage>(afterImagePrefab, afterImageTrm, 20);
    }

    void Start()
    {
        currentJumpCount = jumpCount; //점프 카운트 초기화
    }
    
    void Update()
    {
        if (input.isJump)
        {
            isJump = true;
        }
        
        if(input.isDash && !isDash && canDash && currentDashCooltime <= 0f)
        {
            isDash = true;
            currentDashCooltime = dashCoolTime;
            StartCoroutine(Dash());
        }

        // 대시 쿨타임 줄이기
        if(currentDashCooltime > 0)
        {
            currentDashCooltime -= Time.deltaTime;
            if (currentDashCooltime <= 0) currentDashCooltime = 0;
        }
    }

    //피격 로직
    public void SetHit(Vector2 normal, float power, float delay)
    {
        rigid.gravityScale = 1;
        rigid.velocity = Vector2.zero;
        Vector2 dir = -normal * power + new Vector2(0, 4);
        rigid.AddForce(dir, ForceMode2D.Impulse);
        StartCoroutine(RecoverProcess(delay));
    }

    IEnumerator RecoverProcess(float time)
    {
        isHit = true;
        yield return new WaitForSeconds(time);
        isHit = false;
    }

    IEnumerator Dash()
    {
        Vector2 dir = spriteRenderer.flipX ? transform.right * -1 : transform.right;
        rigid.velocity = Vector2.zero;
        rigid.AddForce(dir * dashPower, ForceMode2D.Impulse);
        rigid.gravityScale = 0;

        float time = 0;
        float afterTime = 0;
        float targetTime = Random.Range(0.02f, 0.06f);
        while(isDash)
        {
            time += Time.deltaTime;
            afterTime += Time.deltaTime;

            if(afterTime >= targetTime){
                AfterImage ai = PoolManager.GetItem<AfterImage>();
                ai.SetSprite(spriteRenderer.sprite, spriteRenderer.flipX, transform.position);
                targetTime = Random.Range(0.02f, 0.06f);
                afterTime = 0;
            }

            if(time >= dashTime){
                isDash = false;
            }
            yield return null;
        }
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 1;
    }

    void FixedUpdate()
    {
        if(isDash) return; //대시중에는 기본 연산은 하지 않는다.

        float xMove = input.xMove;
        if (xMove > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (xMove < 0)
        {
            spriteRenderer.flipX = true;
        }

        isGround = Physics2D.OverlapCircle(
            groundChecker.position, 0.1f, whatIsGround);

        if(isGround){
            currentJumpCount = jumpCount; //땅에 닿으면
        }

        if (isJump && (isGround || currentJumpCount > 0) )
        {
            currentJumpCount--;
            rigid.velocity = Vector2.zero;
            rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            playerAnimation.Jump(); //애니메이션 점프 재생
        }

        isJump = false;
        if(isGround && rigid.velocity.y < 0.1f){
            playerAnimation.JumpEnd(); //점핑애니메이션 끝
        }

        rigid.velocity = new Vector2(xMove * moveSpeed, rigid.velocity.y);
    }
}
