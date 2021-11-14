using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IUseAble
{
    public string useText;

    public abstract void Use(GameObject target);

    protected bool used = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (used) return;

        if(collision.gameObject.CompareTag("Player"))
        {
            //UIManager�� ���ؼ� ���� �ؽ�Ʈ�� �����ֵ����ϰ�
            UIManager.ShowToolTip(useText);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //UIManager�� ���ؼ� ���� �ؽ�Ʈ�� ��������� �Ѵ�
            UIManager.CloseTooltip();
        }
    }
}
