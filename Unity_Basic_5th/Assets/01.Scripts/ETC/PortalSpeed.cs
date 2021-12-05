using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpeed : MonoBehaviour
{
    SpriteRenderer sr;
    Material mat;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        mat = sr.material;
    }

    private void Start()
    {
        mat.SetFloat("speed", 5f);
    }
}
