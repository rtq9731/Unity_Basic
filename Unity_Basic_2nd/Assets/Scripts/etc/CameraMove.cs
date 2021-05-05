using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float maxX = 20f;
    [SerializeField] float minX = 0f;

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        Vector3 nextPos = transform.position + new Vector3(x * moveSpeed * Time.deltaTime, 0, 0);
        nextPos.x = Mathf.Clamp(nextPos.x, minX, maxX);

        transform.position = nextPos;
    }
}
