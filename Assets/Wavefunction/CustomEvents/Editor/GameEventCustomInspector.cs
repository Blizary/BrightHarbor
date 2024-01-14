using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class GameEventCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameEvent myScript = (GameEvent)target;
        if (GUILayout.Button("Trigger Event"))
        {
            myScript.Raise();
        }
    }
}
