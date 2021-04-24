using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteController : MonoBehaviour
{ 
    [SerializeField] [Header("가속도")] float HC = 10;
    [SerializeField] [Header("속도")] private float speed = 0.5f;

    private bool isroll = false;
    private bool isStart = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isStart)
        {
            isStart = true;
            isroll = true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            isroll = false;
        }

        if(isroll)
        {
            transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
            if (speed > 1000)
                return;
            speed += HC;
        }

        if(!isroll)
        {
            transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
            if (speed <= 0.001f)
                return;
            speed -= HC;
        }    

    }
}
