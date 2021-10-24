using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IUseable
{
    public string useText = "";

    public abstract void Use(GameObject target);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            // UI 매니저를 통해 툴팁 텍스트 보여주기
            UIManager.ShowToolTip(useText);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            // UI 매니저를 통해 툴팁 텍스트 보여주기
            UIManager.CloseToolTip();
        }
    }
}
