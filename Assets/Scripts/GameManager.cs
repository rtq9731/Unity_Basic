using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] Slider Slider = null;
    public Transform carTr;
    public Transform flagTr;

    float originDistance = 0f;

    public Text distanceText;

    private void Awake()
    {
        Instance = this;
        originDistance = flagTr.position.x - carTr.position.x;
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    private void Update()
    {
        float distance = flagTr.position.x - carTr.position.x;
        float sliderValue = distance / originDistance;
        Slider.value = sliderValue;
        distance = Mathf.Abs(distance);

        distanceText.text = $"깃발까지의 거리 {distance.ToString("00.00")}M";
    }
}
