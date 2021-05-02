using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CarController : MonoBehaviour
{
    [SerializeField] float speed = 0f;
    [SerializeField] float breakPower = 0f;
    [SerializeField] GameObject image = null;

    float speedRemeber = 0;
    private bool isBreaking = false;

    private void Start()
    {
        speedRemeber = speed;
        speed = 0;
    }
    private void Update()
    {
        if (!isBreaking)
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
            image.transform.Rotate(new Vector3(0, 0, -speed * Time.deltaTime * 50));
        }

        if (Input.GetMouseButtonDown(0) && !isBreaking)
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
