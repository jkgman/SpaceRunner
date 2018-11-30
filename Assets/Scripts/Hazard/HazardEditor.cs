using System.Collections;
using UnityEngine;
using UnityEditor;
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
