using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 50, 50), "Ŭ��"))
        {
            Debug.Log("Ŭ�� ��ư");
        }
    }
}
