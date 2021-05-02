using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonHandler : MonoBehaviour
{

    [SerializeField] GameObject ballPrefab = null;
    [SerializeField] GameObject firePosition = null;
    [SerializeField] Slider gauge = null;
    [SerializeField] float angleSpeed = 60f; // √ ¥Á 20µµ
    [SerializeField] float power = 0f;
    [SerializeField] float maxPower = 0f;
    [SerializeField] float startPower = 0f;
    [SerializeField] Text powerText;
    [SerializeField] Text angleText;

    bool isFire;
    bool isCharging;

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(new Vector3(0, 0, angleSpeed * Time.deltaTime));

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(new Vector3(0, 0, angleSpeed * Time.deltaTime * (-1)));
        }

        if(Input.GetMouseButton(0))
        {
            isCharging = true;
        }

        if(Input.GetMouseButtonUp(0) && !isFire)
        {
            isFire = true;
            GameObject temp = Instantiate(ballPrefab, firePosition.transform.position, Quaternion.identity);
            temp.GetComponent<BallScript>().Shoot(firePosition.transform.right, power);
        }

        if (isCharging && !isFire && power < maxPower)
        {
            power += 500 * Time.deltaTime;
        }

        power = Mathf.Clamp(power, 0, maxPower);
        gauge.value = power / maxPower;


        float z = Mathf.Clamp(transform.rotation.eulerAngles.z, 1, 88);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, z));

        powerText.text = $"Power : {power.ToString("0.0")}";
        angleText.text = $"Angle : {z.ToString("0.0")}°∆";
    }
}
