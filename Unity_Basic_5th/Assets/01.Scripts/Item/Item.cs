using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IUseable
{
    public int value;

    public virtual void Use(GameObject target)
    {
        gameObject.SetActive(false);
    }
}
