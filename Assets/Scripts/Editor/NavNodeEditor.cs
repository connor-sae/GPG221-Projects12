using UnityEditor;
using UnityEngine;

namespace GPG221.AI.Editors
{


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
        if(GUILayout.Button("Update TraversiblePaths"))
        {
            ((NavNode)target).DisconnectObstructedConnections();
        }
    }
}


[CustomEditor(typeof(VisualisedNavNode))]
public class VisualisedNavNodeEditor : NavNodeEditor {}

}