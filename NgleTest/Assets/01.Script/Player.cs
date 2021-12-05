using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerTimeSleep))]
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerActionUI))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerGauge))]
[RequireComponent(typeof(HitEffector))]
[RequireComponent(typeof(PlayerAnim))]
[RequireComponent(typeof(AfterImageScript))]
public class Player : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerTimeSleep playerTimeSleep;
    PlayerMove playerMove;
    PlayerActionUI actionUI;
    PlayerAttack playerAttack;
    PlayerGauge playerGauge;
    HitEffector hitEffector;
    PlayerAnim playerAnim;
    AfterImageScript afterImage;

    public bool isGameOver = false;
    int playerHP = 0;

    private Vector2 oringinPos = Vector2.zero;  

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerTimeSleep = GetComponent<PlayerTimeSleep>();
        playerMove = GetComponent<PlayerMove>();
        actionUI = GetComponent<PlayerActionUI>();
        playerAttack = GetComponent<PlayerAttack>();
        playerGauge = GetComponent<PlayerGauge>();
        hitEffector = GetComponent<HitEffector>();
        playerAnim = GetComponent<PlayerAnim>();
        afterImage = GetComponent<AfterImageScript>();

        oringinPos = transform.position;
    }

    private void Update()
    {
        if(!isGameOver)
        {
            playerMove.MovePlayer(playerTimeSleep.mytimeScale);
        }

        if(playerTimeSleep.IsOverTimeSleep())
        {
            playerTimeSleep.GaugeRefill();
            playerGauge.UpdateGauge(playerTimeSleep.Gauge);
        }

        if (playerInput.bGetMouseButton && !playerTimeSleep.IsOverTimeSleep()) // 마우스가 내려가 있고, 타입 슬립이 끝나지 않음
        {
            actionUI.DoActionUI();

            afterImage.isOnAfterEffect = true;
            if (!playerTimeSleep.TimeSleep())
            {
                afterImage.isOnAfterEffect = false;
                actionUI.RemoveActionUI();
                playerAnim.OnAfterEffect();
                playerGauge.UpdateGauge(playerTimeSleep.Gauge);
            }
        }

        if (playerInput.bGetMouseButtonUP && playerTimeSleep.CanAttack()) // 공격 가능하고, 마우스입력이 떼졌을 때
        {
            playerAttack.Attack(hitEffector);
            playerTimeSleep.ResetMyTimeScale();
            actionUI.RemoveActionUI();
            playerAnim.OffAfterEffect();
            playerAnim.PlayAttackAnim();
        }
    }
}
