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
        if(GUILayout.Button("Right"))
        {
            script.controller.MovementCalc(Vector2.zero,new Vector2(1,0), 0f);
        }
        if(GUILayout.Button("Left"))
        {
            script.controller.MovementCalc(Vector2.zero, new Vector2(-1, 0), 0f);
        }
        if(GUILayout.Button("Jump"))
        {
            script.MovementCalc(Vector2.zero, new Vector2(0, 1), 0f);
        }
        if(GUILayout.Button("Slide"))
        {
            script.MovementCalc(Vector2.zero, new Vector2(0, -1), 0f);
        }
    }
}
