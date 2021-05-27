using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] Text[] countText;
    [SerializeField] Button[] buttons;

    public void RefreshItemCount(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            countText[i].text = $"{arr[i]}°³";
        }
    }

    public void SetItemText(int[] arr)
    {
        RefreshItemCount(arr);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
    }
}
