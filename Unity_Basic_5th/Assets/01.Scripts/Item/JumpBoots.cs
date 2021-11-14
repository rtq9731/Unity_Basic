using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoots : UniqueItem
{
    public override void Use(GameObject target)
    {
        if (!canUse || used) return;

        used = true;

        PlayerMove move = target.GetComponent<PlayerMove>();

        if(move != null)
        {
            move.jumpCount += 1;
            GameManager.ShowDialog(dialogNumber);
            base.Use(target);
        }
    }
}
