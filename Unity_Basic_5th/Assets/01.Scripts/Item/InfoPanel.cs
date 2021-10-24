using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : Interactable
{
    public int dialogCode;

    private bool isInteract = false;

    public override void Use(GameObject target)
    {
        if (isInteract)
        {
            return;
        }

        isInteract = true;

        GameManager.ShowDialog(dialogCode, () => isInteract = false);
    }
}
