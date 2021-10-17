using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyFOV))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyFOV fov = target as EnemyFOV;
        Vector2 fromAngle = fov.CirclePoint(fov.viewAngle * 0.5f);

        Handles.color = Color.white;
        Handles.DrawSolidDisc(fov.transform.position + (Vector3)fromAngle, Vector3.forward, 0.1f); 
    }
}
