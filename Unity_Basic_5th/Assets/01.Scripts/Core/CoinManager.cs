using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;

    void Start()
    {
        PoolManager.CreatePool<Coin>(coinPrefab, transform, 30);        
    }

    public static void PopCoin(Vector3 pos, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Coin coin = PoolManager.GetItem<Coin>();
            coin.PopUp(pos);
        }
    }
}
