using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class WaveText : MonoBehaviour
{
    private TMP_Text tmpText;

    Mesh mesh;
    Vector3[] vertices;

    private void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    //private void StartEffect()
    //{
    //    Debug.Log(tmpText.textInfo.characterCount);
    //    mesh = tmpText.mesh;
    //    vertices = mesh.vertices;
    //    for (int i = 0; i < tmpText.textInfo.characterCount; i++)
    //    {
    //        TMP_CharacterInfo c = tmpText.textInfo.characterInfo[i];

    //        if (!c.isVisible)
    //        {
    //            continue;
    //        }

    //        int idx = c.vertexIndex;
    //        Vector3 offset = Vector3.zero;
    //        for (int j = 0; j < 4; j++)
    //        {
    //            Sequence seq = DOTween.Sequence();

    //            seq.Append(DOTween.To(() => vertices[idx + j], x => vertices[idx + j] = x, new Vector3(vertices[idx + j].x + 5, vertices[idx + j].y), 1));
    //            mesh.vertices = vertices;
    //            seq.Append(DOTween.To(() => vertices[idx + j], x => vertices[idx + j] = x, new Vector3(vertices[idx + j].x - 5, vertices[idx + j].y), 1));
    //            mesh.vertices = vertices;
    //            seq.SetLoops(3).SetEase(Ease.InOutElastic);

    //            seq.OnComplete(() => {
    //                DOTween.To(() => vertices[idx + j], x => vertices[idx + j] = x, new Vector3(0, vertices[idx + j].y - 10), 1);
    //            });
    //        }
    //    }

    //    mesh.vertices = vertices;
    //    tmpText.canvasRenderer.SetMesh(mesh);
    //}


    private void Update()
    {
        // �츮�� ������ ������ ������Ʈ������
        tmpText.ForceMeshUpdate();
        // StartEffect();

        mesh = tmpText.mesh; // ������ Mesh ���� ��������
        vertices = mesh.vertices; // ���ڵ��� ��� ������ �����
                                  //�� ���ڴ� ���� 4���� �������� �����������.

        //for (int i = 0; i < vertices.Length; i++)
        //{
        //    Vector3 offset = Wobble(Time.time + i);
        //    vertices[i] += offset;
        //}

        for (int i = 0; i < tmpText.textInfo.characterCount; i++)
        {
            StartCoroutine(DropText(i));
        }

        //if (Time.time < 3)
        //{
        //    for (int i = 0; i < tmpText.textInfo.characterCount; i++)
        //    {
        //        TMP_CharacterInfo c = tmpText.textInfo.characterInfo[i];

        //        // Null && ���� && \n ���� ��찡 !isVisible
        //        if (!c.isVisible)
        //        {
        //            continue;
        //        }
        //        int idx = c.vertexIndex; // �ڽ��� ���� ��ȣ
        //        Vector3 offset = Wobble(Time.time + i);
        //        for (int j = 0; j < 4; j++)
        //        {
        //            vertices[idx + j] += offset;
        //        }
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < tmpText.textInfo.characterCount; i++)
        //    {
        //    }
        //}

        mesh.vertices = vertices;
        tmpText.canvasRenderer.SetMesh(mesh);
    }

    IEnumerator DropText(int i)
    {
        TMP_CharacterInfo c = tmpText.textInfo.characterInfo[i];

        // Null && ���� && \n ���� ��찡 !isVisible
        if (!c.isVisible)
        {
            yield return null;
        }
        int idx = c.vertexIndex; // �ڽ��� ���� ��ȣ
        for (int j = 0; j < 4; j++)
        {
            vertices[idx + j].y += 0.5f;
            Debug.Log(tmpText.text[i] + vertices[idx + j].y.ToString());
        }

        yield return new WaitForSeconds(i);
    }

    private Vector2 Wobble(float time)
    {
        float x = Mathf.Sin(time * 4f) * 3f; // -3 ~ 3 �ӵ� 4���
        float y = Mathf.Cos(time * 2.2f) * 3f; // -3 ~ 3 �ӵ� 2.2���
        return new Vector2(x, y);
    }    
}
