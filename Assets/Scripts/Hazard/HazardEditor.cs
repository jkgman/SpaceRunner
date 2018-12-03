using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
#if UNITY_EDITOR
[CustomEditor(typeof(HazardChunck))]
public class HazardEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        HazardChunck script = (HazardChunck)target;
        if(GUILayout.Button("Generate"))
        {
            script.Gen();
        }
    }
}
#endif