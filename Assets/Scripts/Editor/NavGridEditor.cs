using UnityEditor;
using UnityEngine;

namespace GPG221.AI.Editors
{


[CustomEditor(typeof(NavGrid))]
public class NavGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Generate"))
        {
            ((NavGrid)target).Clear();
            ((NavGrid)target).Generate();
        }
        if (GUILayout.Button("Clear"))
        {
            ((NavGrid)target).Clear();
        }
    }
}

}