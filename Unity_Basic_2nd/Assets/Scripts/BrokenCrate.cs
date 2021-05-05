using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenCrate : MonoBehaviour
{
    private Rigidbody2D[] childRigid;
    // Start is called before the first frame update
    void Awake()
    {
        childRigid = GetComponentsInChildren<Rigidbody2D>();
    }
    
    public void AddExplosion(Vector3 dir, float power)
    {
        foreach (var item in childRigid)
        {
            item.AddForce(dir * power, ForceMode2D.Impulse);
        }
    }
}
