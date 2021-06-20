using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    public static Dictionary<string, object> pool = new Dictionary<string, object>();
    public static Dictionary<string, GameObject> prefabDictionary = new Dictionary<string, GameObject>();

    /// <summary>
    /// �ʿ��� Pool�� ������ݴϴ�.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="prefab">���� ������</param>
    /// <param name="parent">�θ� Transform</param>
    /// <param name="count">�⺻���� ����</param>
    public static void CreatePool<T>(GameObject prefab, Transform parent, int count = 5)
    {
        Queue<T> q = new Queue<T>();
        for (int i = 0; i < count; i++)
        {
            GameObject temp = GameObject.Instantiate(prefab, parent);
            temp.SetActive(false); // ������� ������Ʈ�� ���� ���·� �������.
            q.Enqueue(temp.GetComponent<T>()); // Queue�� �߰�����. 
        }

        string key = typeof(T).ToString(); // �� Ű���� T �̸��� �� ��.
        pool.Add(key, q); // ��ųʸ����� ( T �� �̸� ) Ű ������ Queue<T>�� �߰��Ѵ�
        prefabDictionary.Add(key, prefab); // �׸��� �߰������� �� ���� ���� ������ prefab�� ����.
    }

    /// <summary>
    /// T�� ������ �ִ� ������Ʈ�� �˸��� Pool���� �̾��ݴϴ�.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetItem<T>() where T : MonoBehaviour
    {
        string key = typeof(T).ToString(); // T���� key���� ����� ���� T.ToString
        T item = null; // ������ item�� �ʱ�ȭ�Ѵ�.

        if(pool.ContainsKey(key)) // ���� key���� �´� pool�� �����Ѵٸ�
        {
            Queue<T> q = (Queue<T>)pool[key]; // pool Dictionary���� Queue<T> �� �ҷ��´�. �⺻������ Object ������ Dictinary�� ������, T�� ����ȯ ���ش�.
            T firstItem = q.Peek(); // pool�� ù��° obj�� Ȯ���ϱ� ���� �ҷ���.

            if(firstItem.gameObject.activeSelf) // firstItem�� Ȱ��ȭ (�����) �̶�� ��� �������� ������ΰ�.
            {
                GameObject prefab = prefabDictionary[key]; // �������� �����´�.
                GameObject g = GameObject.Instantiate(prefab, firstItem.transform.parent); // �������� �����.
                item = g.GetComponent<T>(); // ������ item �� ���ӿ�����Ʈ�� �ش�.
            }
            else // �׷��� �ʴٸ� ��������� ���� ���̴�, ť���� ���� ������.
            {
                item = q.Dequeue();
                item.gameObject.SetActive(true); // Ȱ��ȭ
            }

            q.Enqueue(item); // �ٽ� ť�� �� ���������� ������.
        }

        return item; // ť���� ���� ������
    }
    
}
