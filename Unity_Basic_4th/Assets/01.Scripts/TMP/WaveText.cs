using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Update()
    {
        // �츮�� ������ ������ ������Ʈ������
        tmpText.ForceMeshUpdate();

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
            TMP_CharacterInfo c = tmpText.textInfo.characterInfo[i];
            
            // Null && ���� && \n ���� ��찡 !isVisible
            if(!c.isVisible)
            {
                continue;
            }
            int idx = c.vertexIndex; // �ڽ��� ���� ��ȣ
            Vector3 offset = Wobble(Time.time + i);
            for (int j = 0; j < 4; j++)
            {
                vertices[idx + j] += offset;
            }
        }

        mesh.vertices = vertices;
        tmpText.canvasRenderer.SetMesh(mesh);
    }

    private Vector2 Wobble(float time)
    {
        float x = Mathf.Sin(time * 4f) * 3f; // -3 ~ 3 �ӵ� 4���
        float y = Mathf.Cos(time * 2.2f) * 3f; // -3 ~ 3 �ӵ� 2.2���
        return new Vector2(x, y);
    }    
}
