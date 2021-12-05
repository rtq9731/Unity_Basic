using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeSleep : MonoBehaviour
{
    [SerializeField] float gaugeMinus = 0.1f;
    [SerializeField] float gaugePlus = 0.1f;
    [SerializeField] float timeSleepSpeed = 0.1f;
    [SerializeField] float gauge = 100f;

    bool isOverTimesleep = false;
    bool isAttackable = false;

    public float mytimeScale = 0f;

    public float Gauge
    {
        get { return gauge; }
    }

    public bool TimeSleep()
    {
        mytimeScale = timeSleepSpeed;
        gauge -= gaugeMinus * Time.deltaTime;
        isAttackable = true;

        if (gauge <= 0)
        {
            // RemoveActionUI();
            mytimeScale = 1f;
            isOverTimesleep = true;
            isAttackable = false;
        }

        return isAttackable; 
    }

    public void ResetMyTimeScale()
    {
        mytimeScale = 1f;
    }

    public void GaugeRefill()
    {
        gauge += gaugePlus * Time.deltaTime;
        if (gauge >= 100)
        {
            isOverTimesleep = false;
        }
    }

    public bool IsOverTimeSleep()
    {
        return isOverTimesleep;
    }

    public bool CanAttack()
    {
        return isAttackable;
    }
}
