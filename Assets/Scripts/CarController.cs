using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarController : MonoBehaviour
{
    [SerializeField] float speed = 0f;
    [SerializeField] float breakPower = 0f;

    float speedRemeber = 0;
    private bool isBreaking = false;

    private void Start()
    {
        speedRemeber = speed;
        speed = 0;
    }
    private void Update()
    {
        transform.Translate(new Vector2(speed * Time.deltaTime, 0));

        if (Input.GetMouseButtonDown(0))
            speed = speedRemeber;

        if (Input.GetMouseButtonUp(0))
            isBreaking = true;

        if (isBreaking)
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
            if (speed <= 0.001f)
                return;
            speed -= breakPower;
        }
    }
}
