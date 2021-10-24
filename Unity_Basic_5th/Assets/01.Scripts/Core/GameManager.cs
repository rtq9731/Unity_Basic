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
            return instance != null ? instance.player : null;
        }
    }
    public Transform player;
    public GameObject bloodParticlePrefab;

    public DialogPanel dialogPanel; //다이얼로그 패널 제어용 스크립트
    private Dictionary<int, List<TextVO>> dialogTextDictionary = new Dictionary<int, List<TextVO>>();
    
    private float timeScale = 1f;
    
    public static float TimeScale
    {
        get
        {
            return instance != null ? instance.timeScale : 1;
        }
        set
        {
            instance.timeScale = Mathf.Clamp( value, 0, 1);
        }
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

    private int coinCount = 0;
    public static void AddCoin(int coin)
    {
        instance.coinCount++;
        // 지금은 코인 증가만 / 나중엔 로직추가
    }

    public static void RemoveCoin(int value)
    {
        // 여기도 UI 갱신 필요
        instance.coinCount = Mathf.Clamp(instance.coinCount - value, 0, instance.coinCount);
    }

    private void Start()
    {
        PoolManager.CreatePool<BloodParticle>(bloodParticlePrefab, transform, 10);
        
        //이건 테스트 코드
        //dialogPanel.StartDialog(dialogTextDictionary[0]);
    }

    public static void ShowDialog(int index, Action callback = null)
    {
        if(index >= instance.dialogTextDictionary.Count)
        {
            return;
        }

        //해당 인덱스 의 대화를 재생하도록 함.
        instance.dialogPanel.StartDialog(instance.dialogTextDictionary[index], callback);
    }
}