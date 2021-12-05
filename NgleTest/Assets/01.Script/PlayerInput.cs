using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool bGetMouseButton = false;
    public bool bGetMouseButtonUP = false;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            bGetMouseButton = true;
        }
        else
        {
            bGetMouseButton = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            bGetMouseButtonUP = true;
        }
        else
        {
            bGetMouseButtonUP = false;
        }
    }
}
