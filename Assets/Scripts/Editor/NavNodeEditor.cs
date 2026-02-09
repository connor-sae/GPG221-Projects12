using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NavNode))]
public class NavNodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Update Obstructed"))
        {
            ((NavNode)target).UpdateBounds();
        }
    }
}
