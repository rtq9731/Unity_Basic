using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    [SerializeField] float ScaleFactor = 1f;
    private Vector3 beforPos;

    private void Start()
    {
        beforPos = Camera.main.transform.position;
    }

    private void Update()
    {
        Vector3 delta = Camera.main.transform.position - beforPos;

        transform.Translate(delta * ScaleFactor);

        beforPos = Camera.main.transform.position;
    }
}
