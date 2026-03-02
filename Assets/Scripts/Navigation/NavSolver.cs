using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GPG221.AI.Solvers
{

    public class NavSolver : MonoBehaviour
    {
        public static NavSolver instance;
        protected virtual void Awake()
        {
            if(instance == null)
            {
                instance = this;

            }else
                Destroy(gameObject);
        }
        [Tooltip("The Threshold for connecting neighboring nodes to the target / origin position")]
        // should roughly equal 0.75 the distance between any 2 nodes
        public float targetGridStitchThreshold = 2f;
        [SerializeField] public NavNode[] navNodes;

        
        public virtual void GeneratePath(Vector3 Origin, Vector3 Destination, out NavPath navPath)
        {
            navPath = new();
            navPath.Fail("Generate called from abstract base navSolver");
            Debug.LogWarning("Not implemented");
        }

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
            //OnDrawGizmos();
        }


        public void RegisterNavNodes(NavNode[] addNodes)
        {
            List<NavNode> newNodes = new();
            newNodes = navNodes.ToList();
            
            foreach(NavNode addNode in addNodes)
            {
                if (!newNodes.Contains(addNode))
                    newNodes.Add(addNode);
            }
            
            navNodes = newNodes.ToArray();
        }

        public bool IsActive()
        {
            return NavUtil.activeSolver == this;
        }

        #region visualization


        [Header("Gizmos")]
        [SerializeField] private bool visualizeNodePaths;
        [SerializeField] private bool usePathWidth;
        [SerializeField] private bool visualizeNodeRadius;
        private void OnDrawGizmos()
        {
            if(IsActive()  && navNodes != null)
            {
                foreach(NavNode node in navNodes)
                {
                    if(node == null) return;

                    if(visualizeNodeRadius)
                    {
                        if(node.obstructed)
                            Gizmos.color = Color.red;
                        else
                            Gizmos.color = Color.green;

                        Gizmos.DrawWireSphere(node.transform.position, node.obstructionRadius);
                    }
                    
                    if(visualizeNodePaths)
                    {   
                        if(usePathWidth)
                            Gizmos.color = Color.yellow;
                        else
                            Gizmos.color = Color.white;
                        
                        foreach(NavNode otherNode in node.linkedNodes)
                        {
                            if(usePathWidth)
                            {
                                Vector3 direction = otherNode.transform.position - node.transform.position;
                                float angle = Mathf.PI * .5f; //90 degrees
                                float x = direction.normalized.x;
                                float z = direction.normalized.z;
                                Vector3 perpindicular = new Vector3(x*Mathf.Cos(angle) - z*Mathf.Sin(angle), 0,
                                                                    x*Mathf.Sin(angle) + z*Mathf.Cos(angle));

                                float pathRad = node.obstructionRadius * node.pathObstructionFactor;
                                Gizmos.DrawRay(node.transform.position + perpindicular * pathRad, direction);
                                Gizmos.DrawRay(node.transform.position + perpindicular * -pathRad, direction);

                            }else
                                Gizmos.DrawLine(node.transform.position, otherNode.transform.position);
                        }
                    }
                }
                Gizmos.color = new Color(0.2f, 0.3f, 1f);
                foreach(NavNode selectedNode in Selection.GetFiltered<NavNode>(SelectionMode.ExcludePrefab))
                {
                    Gizmos.DrawSphere(selectedNode.transform.position, 0.3f);
                }
            }

        }


        #endregion
    }
}
