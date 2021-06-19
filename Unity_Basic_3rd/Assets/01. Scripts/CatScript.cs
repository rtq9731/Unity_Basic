using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CatScript : MonoBehaviour
{
    [Header("����� ȭ��ǥ ���� ����")]
    public Transform arrow;
    public bool isLookAt = false;
    public int arrowIndex = 0;

    public float timeBetChange = 2f;
    private float changeTimer = 0;

    private Vector3 originPoint;

    [Header("��δ�")]
    public float feeling = 30f;
    public Sprite[] sprites;
    private SpriteRenderer sr;

    public int[] feelAmount = new int[4];

    private Dictionary<Items, float> itemDictionary = new Dictionary<Items, float>();

    [SerializeField] Slider slider;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        originPoint = arrow.position;
        arrow.gameObject.SetActive(false);

        for (int i = 0; i < feelAmount.Length; i++)
        {
            int idx = Random.Range(0, feelAmount.Length - i);
            itemDictionary.Add((Items)i, feelAmount[idx]);

            int temp = feelAmount[feelAmount.Length - 1 - i];
            feelAmount[feelAmount.Length - 1 - i] = feelAmount[idx];
            feelAmount[idx] = temp;
        }
    }

    public void ArrowSequence()
    {
        isLookAt = true;
        arrow.gameObject.SetActive(true);
    }

    void Update()
    {
        if (isLookAt)
        {
            changeTimer += Time.deltaTime;
            if (changeTimer >= timeBetChange)
            {
                arrowIndex = (arrowIndex + 1) % 3;
                arrow.position = originPoint + new Vector3(arrowIndex * 3, 0, 0);
                changeTimer = 0;
            }

            if (Input.GetButtonDown("Jump"))
            {
                StopLookAt();
            }
        }
    }

    public void StopLookAt()
    {
        isLookAt = false;
        GameManager.OpenCup(arrowIndex);
    }

    public void Give(Items item)
    {
        float value = itemDictionary[item];
        feeling += value;

            switch (value)
            {
                case -20:
                    GameManager.SetCatText("�ش��(п����)", 2f);
                    Camera.main.DOShakePosition(1f, 1f);
                    break;
                case -10:
                    GameManager.SetCatText("�߳�(����)", 2f);
                    Camera.main.DOShakePosition(0.5f, 0.5f);
                    break;
                case 10:
                    GameManager.SetCatText("���δ��� ���� �׷� ������ ������ �ϴ�", 2f);
                    break;
                case 20:
                    GameManager.SetCatText("���δ��� ���� �����ѵ� �ϴ�", 2f);
                    break;
            }

        int idx = (int)Mathf.Clamp(Mathf.Floor(feeling / 30), 0, sprites.Length - 1);
        sr.sprite = sprites[idx];

        slider.value = feeling;

        Invoke("ResetCup", 2f);
    }
}
