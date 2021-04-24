using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform carTr;
    public Transform flagTr;

    public Text distanceText;

    private void Update()
    {
        float distance = flagTr.position.x - carTr.position.x;
        distance = Mathf.Abs(distance);

        distanceText.text = $"��߱����� �Ÿ� {distance.ToString("00.00")}M";
    }
}
