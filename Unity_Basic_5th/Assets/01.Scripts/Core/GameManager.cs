using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public static Transform Player
    {
        get
        {
            if(instance != null)
            {
                return instance.player;
            }
            else
            {
                return null;
            }
           
        }
    }
    public Transform player;
    public GameObject bloodParticlePrefab;

    public DialogPanel dialogPanel; //���̾�α� �г� ����� ��ũ��Ʈ
    private Dictionary<int, List<TextVO>> dialogTextDictionary = new Dictionary<int, List<TextVO>>();
    
    private float timeScale = 1f;
    
    public static float TimeScale
    {
        get
        {
            if(instance != null)
            {
                return instance.timeScale;
            }
            else
            {
                return 0f;
            }
                      
        }
        set
        {
            instance.timeScale = Mathf.Clamp( value, 0, 1);
        }
    }

    private int coinCount = 0;
    public static void AddCoin(int value)
    {
        UIManager.SetCoinText(instance.coinCount);
        //���⼭�� �ܼ��� ������ ������Ű�⸸ ������ ���߿� ���⿡ UI�� �����ϴ� ������ ���� �Ѵ�.
        instance.coinCount += value;
    }

    public static void RemoveCoin(int value)
    {
        UIManager.SetCoinText(instance.coinCount);
        //�翬�� ���⼭�� UI����
        instance.coinCount = Mathf.Clamp(instance.coinCount - value, 0, instance.coinCount);
    }


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("<color=ffcccc>WARN</color>: There are more than one GameManager running in same scene");
        }

        instance = this;

        TextAsset dJson = Resources.Load("dialogText") as TextAsset;
        GameTextDataVO textData = JsonUtility.FromJson<GameTextDataVO>(dJson.ToString());

        foreach(DialogVO vo in textData.list)
        {
            dialogTextDictionary.Add(vo.code, vo.text);
        }
    }

    private void Start()
    {
        PoolManager.CreatePool<BloodParticle>(bloodParticlePrefab, transform, 10);

        UIManager.SetCoinText(coinCount);
        
        //�̰� �׽�Ʈ �ڵ�
        //dialogPanel.StartDialog(dialogTextDictionary[0]);
    }

    public static void ShowDialog(int index, Action callback = null)
    {
        if(index >= instance.dialogTextDictionary.Count)
        {
            return;
        }

        //�ش� �ε��� �� ��ȭ�� ����ϵ��� ��.
        instance.dialogPanel.StartDialog(instance.dialogTextDictionary[index], callback);
    }
}