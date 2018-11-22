using System.Collections;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(PlayerHandle))]
public class PlayerEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PlayerHandle script = (PlayerHandle)target;
        if(GUILayout.Button("Hit"))
        {
            script.Slow();
        }
    }
}
