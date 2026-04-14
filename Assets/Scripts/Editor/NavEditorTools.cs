using UnityEngine;
using UnityEditor;
using Westhouse.GPG221.AI.Navigation;

namespace Wesyhouse.GPG221.AI.Editors
{
public class NavEditorTools  

    {
        [MenuItem("NavTools/Stitch")]
        static void StitchSelected()
        {
            NavNode[] nodes = Selection.GetFiltered<NavNode>(SelectionMode.ExcludePrefab);
            if(nodes.Length <= 1)
                Debug.LogError("Cannot stitch less than 2 Nodes");

            NavUtil.StitchNodes(false, nodes);
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
        [MenuItem("NavTools/Deep Pluck")]
        static void DeepPluckSelected()
        {
            NavNode[] nodes = Selection.GetFiltered<NavNode>(SelectionMode.ExcludePrefab);
            if(nodes.Length <= 0)
                Debug.LogError("no Nodes Selected");

            foreach(NavNode node in nodes)
            {
                NavUtil.PluckNode(node);
                foreach(NavNode allNode in NavUtil.activeSolver.navNodes)
                {
                    allNode.linkedNodes.Remove(node);
                }
            }
        }

        [MenuItem("NavTools/Ensure All Paths")]
        static void EnsureAll()
        {

            foreach(NavNode node in NavUtil.activeSolver.navNodes)
            {
                node.EnsureLinks();
            }
        }

        [MenuItem("NavTools/Register")]
        static void RegisterSelected()
        {
            NavNode[] nodes = Selection.GetFiltered<NavNode>(SelectionMode.ExcludePrefab);
            if(nodes.Length <= 0)
                Debug.LogError("no Nodes Selected");

            NavUtil.activeSolver.RegisterNavNodes(nodes);
        }
    }

    public class SmartStitchWindow : EditorWindow {

        public float distance = 1;

        [MenuItem("NavTools/Smart Stitch")]
        private static void ShowWindow() {
            
            SmartStitchWindow win = GetWindow<SmartStitchWindow>();
        }
    
        private void OnGUI() {
            
            GUILayout.Space(20);
            if(GUILayout.Button("Calibrate max Distance"))
            {
                float maxDistance = 0;
                NavNode[] nodes = Selection.GetFiltered<NavNode>(SelectionMode.ExcludePrefab);
                for(int i = 1; i < nodes.Length; i++)
                {
                    float dist = Vector3.Distance(nodes[i].transform.position, nodes[i-1].transform.position);
                    if(dist > maxDistance)
                        maxDistance = dist;
                }
                distance = maxDistance;
            }
            GUILayout.Space(20);
            EditorGUILayout.LabelField("Distance");
            distance = EditorGUILayout.FloatField(distance);
            GUILayout.Space(10);
            if(GUILayout.Button("Stitch Selected"))
            {
                foreach(NavNode node in Selection.GetFiltered<NavNode>(SelectionMode.ExcludePrefab))
                {
                    NavUtil.StitchByDistance(node, distance, false);
                    
                }
            }
        }
    }
}