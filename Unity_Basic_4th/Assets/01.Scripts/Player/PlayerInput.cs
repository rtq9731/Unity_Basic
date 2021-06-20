using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float xMove { get; private set; }
    public bool isJump { get; private set; }
    public bool isDash { get; private set; }

    public bool isAttack { get; private set; }

    private void Update()
    {
        // GetAxis는 입력이 0 ~ 1
        // GetAxisRaw는 입력이 0, 1
        xMove = Input.GetAxisRaw("Horizontal");
        isJump = Input.GetButtonDown("Jump");
        isDash = Input.GetButtonDown("Dash");
        isAttack = Input.GetButtonDown("Fire1");
    }
}
