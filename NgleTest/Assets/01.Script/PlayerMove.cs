using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    public void MovePlayer(float myTimeScale)
    {
        transform.Translate(transform.right * myTimeScale * Time.deltaTime * speed);
    }
}
