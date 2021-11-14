using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public bool isTriggered = false; //Ȱ���� �Ǿ��°�?
    public int dialogNumber; //�� �� ���̾�α׸� ����� �Ŵ�?
    public BossAI targetBoss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggered) return;
        if(collision.gameObject.CompareTag("Player"))
        {
            //ī�޶� �ܾƿ� ȿ���� ����Ű�
            CamEffectManager.instance.SetCamSize(7, 1f);
            isTriggered = true;
            GameManager.ShowDialog(dialogNumber, EndCallBack);
        }
    }

    private void EndCallBack()
    {
        GameManager.TimeScale = 0;
        //ī�޶� ������� �����ְ�
        CamEffectManager.instance.SetCamSize(5, 1f, () =>
        {
            GameManager.TimeScale = 1;
            targetBoss.StartBoss();
        });

        //���� HP�� �����ְ�
        UIManager.ShowBossHPBar();

    }
}
