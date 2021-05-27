using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum Items
{
    Chu,
    Bear,
    Fish,
    Meat
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] Text catStatusText;
    [SerializeField] Text msgText;
    [SerializeField] Button btnStart;
    [SerializeField] Button btnShuffle;

    [Header("������ ��� ����")]
    [SerializeField] Transform  dropPoints;
    [SerializeField] Item[] dropItems;

    [Header("�ܺ� ���ӿ�����Ʈ ���ٿ�")]
    [SerializeField] ItemPanel itemPanel;
    [SerializeField] int[] count = new int[4];

    Button[] ItemBtns;

    private int shuffleItemCount = 0;
    private int shuffleReadyCount = 0;

    private AudioSource audioSource = null;

    void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dropItems = dropPoints.GetComponentsInChildren<Item>();
        foreach (var item in dropItems)
        {
            item.gameObject.SetActive(false);
        }

        ItemBtns = itemPanel.GetComponentsInChildren<Button>();
        foreach (var item in ItemBtns)
        {
            item.interactable = false;
        }

        btnShuffle.interactable = false;

        GameManager.SetMsgText("���� ���� ��ư�� ���� �����ϼ���", 3f);
    }

    public void GameStart()
    {
        GameManager.SetMsgText("���� 3���� �����ϼ���", 2f);
        btnStart.enabled = false;
        //btnShuffle.enabled = true;

        for (int i = 0; i < count.Length; i++)
        {
            count[i] = 6;
        }

        foreach (var item in ItemBtns)
        {
            item.interactable = true;
        }

        itemPanel.SetItemText(count);

    }

    public void DropItem(int items)
    {
        if (shuffleItemCount >= 3)
        {
            GameManager.SetMsgText("�̹� �ִ밹����ŭ ����߾��", 2f);
            return;
        }

        if(count[items] <= 0){
            GameManager.SetMsgText("�ش� �������� ��� �����߾��", 2f);
            return;
        }

        Item item = dropItems[shuffleItemCount];
        item.gameObject.SetActive(true);

        item.SetItem((Items)items);
        item.DropItem(dropPoints.position.x);
        shuffleItemCount++;
        count[items]--;

        itemPanel.RefreshItemCount(count);
    }

    public static void SetMsgText(string text, float time = 1f)
    {
        Instance.msgText.DOKill();
        Instance.audioSource.Stop();
        Instance.msgText.text = "";
        Instance.audioSource.Play();
        Instance.msgText.DOText(text, time).OnComplete(() => { Instance.audioSource.Stop(); });
    }

    public static void SetCatText(string text, float time = 1f)
    {
        Instance.msgText.text = "";
        Instance.audioSource.Play();
        Instance.msgText.DOText(text, time).OnComplete(() => { Instance.audioSource.Stop(); });
    }

    public static void DropComplete()
    {
        ++Instance.shuffleReadyCount;
        if(Instance.shuffleReadyCount >= 3)
        {
            Instance.btnShuffle.interactable = true;
        }
    }

}
