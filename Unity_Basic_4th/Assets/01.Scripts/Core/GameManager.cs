using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Transform Player
    {
        get
        {
            return Instance.player;
        }
    }

    public Transform player;
    public GameObject bloodParticlePrefab;

    private void Awake()
    {
#if UNITY_EDITOR
        if (Instance != null)
        {
            Debug.LogError("��� : �ټ��� ���ӸŴ����� ������");
        }
#endif
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void OnApplicationQuit()
    {
        Destroy(this);
    }

    private void Start()
    {
        PoolManager.CreatePool<BloodParticle>(bloodParticlePrefab, this.transform);
    }




}
