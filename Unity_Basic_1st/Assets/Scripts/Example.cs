using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 50, 50), "클릭"))
        {
            Debug.Log("클릭 버튼");
        }
    }
}
