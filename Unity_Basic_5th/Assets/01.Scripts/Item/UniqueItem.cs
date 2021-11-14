using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UniqueItem : Item
{
    public int dialogNumber = 5;
    protected bool canUse = false;
    protected bool used = false;

    private void Start()
    {
        canUse = false;
        StartCoroutine(OpenDelay());
    }

    IEnumerator OpenDelay()
    {
        yield return new WaitForSeconds(1f);
        canUse = true;
    }

}
