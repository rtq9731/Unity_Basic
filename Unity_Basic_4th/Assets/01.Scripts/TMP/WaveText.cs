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
        // 우리가 변경한 값들을 업데이트시켜줌
        tmpText.ForceMeshUpdate();
        // StartEffect();

        mesh = tmpText.mesh; // 글자의 Mesh 정보 가져오기
        vertices = mesh.vertices; // 글자들의 모든 정점을 끌어옴
                                  //각 글자는 전부 4개의 정점으로 만들어져있음.

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

        //        // Null && 띄어쓰기 && \n 등의 경우가 !isVisible
        //        if (!c.isVisible)
        //        {
        //            continue;
        //        }
        //        int idx = c.vertexIndex; // 자신의 정점 번호
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

        // Null && 띄어쓰기 && \n 등의 경우가 !isVisible
        if (!c.isVisible)
        {
            yield return null;
        }
        int idx = c.vertexIndex; // 자신의 정점 번호
        for (int j = 0; j < 4; j++)
        {
            vertices[idx + j].y += 0.5f;
            Debug.Log(tmpText.text[i] + vertices[idx + j].y.ToString());
        }

        yield return new WaitForSeconds(i);
    }

    private Vector2 Wobble(float time)
    {
        float x = Mathf.Sin(time * 4f) * 3f; // -3 ~ 3 속도 4배속
        float y = Mathf.Cos(time * 2.2f) * 3f; // -3 ~ 3 속도 2.2배속
        return new Vector2(x, y);
    }    
}
