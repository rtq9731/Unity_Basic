using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] CatScript cat;

    Button[] ItemBtns;

    private int shuffleItemCount = 0;
    private int shuffleReadyCount = 0;
    private int shuffleCount = 0;

    private Item[] shuffledItems;

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
        shuffledItems = new Item[3];
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

    public void Shuffle()
    {
        if (shuffleItemCount < 3)
        {
            GameManager.SetMsgText("�������� 3�� ����� ���డ���մϴ�.", 1.5f);
            return;
        }

        if (shuffleReadyCount < 3)
        {
            GameManager.SetMsgText("��� ��ٷȴ� �õ��ϼ���", 1.5f);
            return;
        }

        btnShuffle.interactable = false; //���� ��ư ����ְ�

        List<int>[] lists = new List<int>[3];
        for (int i = 0; i < lists.Length; i++)
        {
            lists[i] = new List<int>();
        }
        for (int i = 0; i < 8; i++)
        {
            //�� �ѹ����� 0,1,2�� ��ġ�� �ʰ� lists[0,1,2]��°�� Add ���ָ� ��.
            //���ڸ��� ���� ������ �ȵǴϱ� �����ִ� ��ġ�ϰ�� �ٸ���
            List<int> locList = new List<int> { 0, 1, 2 };

            for (int j = 0; j < lists.Length; j++)
            {
                if (i == 0)
                {
                    lists[j].Add(j); //ó������ ������ġ �״�� �ְ�
                }
                else
                {
                    List<int> clone = locList;
                    if (clone.Count > 1)
                        clone.Remove(lists[j][lists[j].Count - 1]);

                    int idx = Random.Range(0, clone.Count); // 0,2
                    lists[j].Add(clone[idx]);

                    locList.Remove(clone[idx]);
                }
            }
        }

        for (int i = 0; i < lists.Length; i++)
        {
            dropItems[i].CupAndShake(lists[i]);
        }

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
    public void Shuffle()
    {
        if(shuffleItemCount < 3)
        {
            GameManager.SetMsgText("�������� 3�� ��� �� ���డ���մϴ�.", 1.5f);
            return;
        }

        if(shuffleReadyCount > 3)
        {
            GameManager.SetMsgText("��� ��ٷȴ� �õ��ϼ���", 1.5f);
            return;
        }

        btnShuffle.interactable = false;

        List<int>[] lists = new List<int>[3];
        for (int i = 0; i < lists.Length; i++)
        {
            lists[i] = new List<int>();
        }

        for (int i = 0; i < 8; i++)
        {
            List<int> locList = new List<int> { 0, 1, 2 };

            for (int j = 0; j < lists.Length; j++)
            {
                if (i == 0)
                {
                    lists[j].Add(j);
                }
                else
                {
                    List<int> clone = locList.ToList();
                    if (clone.Count > 1)
                        clone.Remove(lists[j][lists[j].Count - 1]);

                    int idx = Random.Range(0, clone.Count);
                    lists[j].Add(clone[idx]);

                    locList.Remove(clone[idx]);
                }
            }
        }

        for (int i = 0; i < lists.Length; i++)
        {
            int idx = lists[i].Last();
            shuffledItems[idx] = dropItems[i];
        }

        for (int i = 0; i < lists.Length; i++)
        {
            dropItems[i].CupAndShake(lists[i]);
        }

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

    public static void ShuffleComplete()
    {
        Instance.shuffleCount++;
        if(Instance.shuffleCount >= 3)
        {
            //����̰� �۾�
            Instance.cat.ArrowSequence();
        }
    }

    public static void OpenCup(int idx)
    {
        Instance.shuffledItems[idx].Open();
    }

    public static void GiveItemToCat(Items items)
    {
        Instance.cat.Give(items);
    }
}
