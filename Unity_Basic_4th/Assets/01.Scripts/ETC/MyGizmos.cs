using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{
    [SerializeField] Color color;
    public float radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
