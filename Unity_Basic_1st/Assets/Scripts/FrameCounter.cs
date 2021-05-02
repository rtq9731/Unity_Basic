using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameCounter : MonoBehaviour
{
    [SerializeField]
    private Text frameText;
    float frame = 0;
    float second = 0;

    private void Update()
    {
        frame++;
        second += Time.deltaTime;

        if(second >= 1)
        {
            frameText.text = $"<color=\"#a0f0ff\">{frame}</color><size=\"45\">FPS</size>";
            second = 0;
            frame = 0;
        }
    }

}
