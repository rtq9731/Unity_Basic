using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTest : MonoBehaviour
{
    void Start()
    {
        int[] arr = GetPrimeNumber(10);
     
        for (int i = 0; i < arr.Length; i++)
        {
            if (i < 0) break;
            Debug.Log(i);
        }
    }

    int[] GetPrimeNumber(int a)
    {
        int[] returnArr = new int[a];
        int idx = 0;
        for (int i = 2; i <= a; i++)
        {
            bool isPrime = true;
            for (int j = 2; j < i; j++)
            {
                if (i % j == 0)
                {
                    isPrime = false;
                    break;
                }
            }
            if (isPrime)
            {
                returnArr[idx] = i;
                idx++;
            }
        }
        returnArr[idx] = -1;
        return returnArr;
    }
}

