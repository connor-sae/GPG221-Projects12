using UnityEngine;
using UnityEditor;

namespace GPG221.AI.Editors
{
public class NavEditorTools  

    {
        [MenuItem("NavTools/Stitch")]
        static void StitchSelected()
        {
            NavNode[] nodes = Selection.GetFiltered<NavNode>(SelectionMode.ExcludePrefab);
            if(nodes.Length <= 1)
                Debug.LogError("Cannot stitch less than 2 Nodes");

            NavUtil.StitchNodes(nodes:nodes);
        }

        [MenuItem("NavTools/Sever")]
        static void SeverSelected()
        {
            NavNode[] nodes = Selection.GetFiltered<NavNode>(SelectionMode.ExcludePrefab);
            if(nodes.Length <= 1)
                Debug.LogError("Cannot sever less than 2 Nodes");

            NavUtil.SeverNodes(nodes);
        }

        [MenuItem("NavTools/Pluck")]
        static void PluckSelected()
        {
            NavNode[] nodes = Selection.GetFiltered<NavNode>(SelectionMode.ExcludePrefab);
            if(nodes.Length <= 0)
                Debug.LogError("no Nodes Selected");

            foreach(NavNode node in nodes)
            {
                NavUtil.PluckNode(node);
            }
        }
    }
}