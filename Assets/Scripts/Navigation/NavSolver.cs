using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class NavSolver : MonoSingleton<NavSolver>
{
    public float targetGridThreshold = 1f;
    private NavNode[] navNodes;

    
    public abstract void GeneratePath(Vector3 Origin, Vector3 Destination, out NavPath navPath);

    public void ReRegisterAllNavNodes()
    {
        navNodes = FindObjectsByType<NavNode>(FindObjectsSortMode.None);
    }

    public void DeRegisterNavNodes(NavNode[] removeNodes)
    {
        List<NavNode> newNodes = new();

        
        foreach(NavNode registerednode in navNodes) // only copy nodes that should not be removed
        {
            if(!removeNodes.Contains(registerednode)) // if the node sholud not be removed
                newNodes.Add(registerednode);
        }

        navNodes = newNodes.ToArray();
    }

    public void RegisterNavNodes(NavNode[] addNodes)
    {
        NavNode[] oldNodes = navNodes;
        navNodes = new NavNode[oldNodes.Length + addNodes.Length];
        
        for(int i = 0; i < oldNodes.Length; i++)
        {
            navNodes[i] = oldNodes[i];
        } 

        for(int j = 0; j < addNodes.Length; j++)
        {
            navNodes[j + oldNodes.Length] = addNodes[j];
        }

    }

    #region visualization


    [Header("Gizmos")]
    [SerializeField] private bool VisualizeNodePaths;
    [SerializeField] private bool VisualizeNodeRadius;
    private void OnDrawGizmos()
    {
        foreach(NavNode node in navNodes)
        {
            if(VisualizeNodeRadius)
            {
                if(node.obstructed)
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.green;

                Gizmos.DrawWireSphere(node.transform.position, node.radius);
            }
            
            if(VisualizeNodePaths)
            {
                Gizmos.color = Color.white;
                foreach(NavNode otherNode in node.linkedNodes)
                {
                    Gizmos.DrawLine(node.transform.position, otherNode.transform.position);
                }
            }
        }
    }


    #endregion
}
