using UnityEditor;
using UnityEngine;
using Westhouse.GPG221.AI.Navigation;

namespace Westhouse.GPG221.AI.Editors
{


[CustomEditor(typeof(NavGrid))]
public class NavGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Generate"))
        {
            ((NavGrid)target).ReGenerate();
        }
        if (GUILayout.Button("Clear Obstructed"))
        {
            ((NavGrid)target).DestroyObstructedNodes();
        }
        if (GUILayout.Button("Clear All"))
        {
            ((NavGrid)target).ClearChildren();
        }
    }
}

}