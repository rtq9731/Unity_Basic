using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGauge : MonoBehaviour
{
    [SerializeField] Transform timesleepGauge = null;

    public void UpdateGauge(float gauge)
    {
        timesleepGauge.localScale = new Vector3(gauge / 100, 1, 1);
    }
}
