using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attackable : MonoBehaviour
{
    public abstract void GetAttack(HitEffector hitEffector);
}
