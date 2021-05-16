using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] Text catStatusText;
    [SerializeField] Text msgText;
    
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
        GameManager.SetMsgText("게임 시작 버튼을 눌러 시작하세요", 3f);
    }

    public static void SetMsgText(string text, float time = 1f)
    {
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

}
