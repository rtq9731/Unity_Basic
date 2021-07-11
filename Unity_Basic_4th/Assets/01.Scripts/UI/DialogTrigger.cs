using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] int code;

    bool isRead = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isRead)
        {
            return;
        }

        if(collision.CompareTag("Player"))
        {
            isRead = true;
            GameManager.ShowDialogIndex(code);
        }
    }
}
