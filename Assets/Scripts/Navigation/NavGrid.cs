using System.Collections.Generic;
using UnityEngine;

namespace GPG221.AI
{

    [RequireComponent(typeof(BoxCollider))]
    public class NavGrid : MonoBehaviour
    {
        private new BoxCollider collider;

        [SerializeField] private Vector2Int gridResolution = new Vector2Int(10, 10);
        [SerializeField] private float NodeRadiusFactor = 1;
        [SerializeField] private bool allowDiagonals = true;
        [SerializeField] private LayerMask obstructionMask;
        [SerializeField] private bool destroyObstructed;
        [SerializeField] private NavNode usePrefab;

        private NavNode[,] nodes;
        List<NavNode> linearNodes;

        public void ReGenerate()
        {
            DeRegisterChildNodes();
            ClearChildren();
            GenerateNewNodes();

            NavUtil.activeSolver.RegisterNavNodes(linearNodes.ToArray());

            if(destroyObstructed)
                DestroyObstructedNodes();
        }

        private void DeRegisterChildNodes()
        {
            List<NavNode> childNodes = new();
            for(int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).TryGetComponent(out NavNode node))
                {
                    childNodes.Add(node);
                }
            }
            NavUtil.activeSolver.DeRegisterNavNodes(childNodes.ToArray());
        }

        private NavNode CreateNavNode(string name)
        {
            if(usePrefab == null)
                return new GameObject(name, typeof(NavNode)).GetComponent<NavNode>();
            else
            {
                GameObject obj = Instantiate(usePrefab.gameObject);
                obj.name = name;
                return obj.GetComponent<NavNode>();
            }
        }

        private void GenerateNewNodes()
        {
            nodes = new NavNode[gridResolution.x, gridResolution.y];

            linearNodes = new();

            if(!TryGetComponent(out collider))
            {
                Debug.LogError("NavGrid requires a box collider component");
                return;
            }
            Bounds bounds = collider.bounds;
            float dX = bounds.size.x / gridResolution.x;
            float dZ = bounds.size.z / gridResolution.y;

            for (int x = 0; x < gridResolution.x; x++)
            {
                for (int y = 0; y < gridResolution.y; y++)
                {
                    // Create new Nav Nodes
                    NavNode node = CreateNavNode($"Nav Node ({x}, {y})");
                    node.transform.parent = transform;

                    //set position
                    float xPos = bounds.min.x + (0.5f + x) * dX;
                    float zPos = bounds.min.z + (0.5f + y) * dZ;
                    node.transform.position = new Vector3(xPos, transform.position.y, zPos);


                    //test if obstructed
                    node.obstructionRadius = NodeRadiusFactor * (dX + dZ) * 0.25f;
                    node.obstructionMask = obstructionMask;
                    node.UpdateBounds();
                    

                    //connect to EXISTING previous nodes
                    if(x > 0)
                    {
                        NavUtil.StitchNodes(node, nodes[x-1, y]);

                        if(y > 0)
                        {
                            NavUtil.StitchNodes(node, nodes[x, y-1]);
                        }

                        if (allowDiagonals)
                        {
                            if (y + 1 < gridResolution.y)
                                NavUtil.StitchNodes(node, nodes[x - 1, y + 1]);

                            if (y  > 0)
                                NavUtil.StitchNodes(node, nodes[x - 1, y - 1]);
                        }
                    }
                    else
                    if(y  > 0)
                    {
                        NavUtil.StitchNodes(node, nodes[x, y-1]);
                    }
                    

                    nodes[x, y] = node;
                    linearNodes.Add(node);
                }
            }
        }

        public void ClearChildren()
        {
            DeRegisterChildNodes();

            foreach (NavNode node in GetComponentsInChildren<NavNode>())
            {
                DestroyImmediate(node.gameObject);
                nodes = new NavNode[0,0];
            }
        }

        public void DestroyObstructedNodes()
        {
            List<NavNode> destroyNodes = new();
            foreach(NavNode node in linearNodes)
            {
                if(node.obstructed)
                    destroyNodes.Add(node);
            }

            NavUtil.activeSolver.DeRegisterNavNodes(destroyNodes.ToArray());

            foreach(NavNode node in destroyNodes)
            {
                linearNodes.Remove(node);
                DestroyImmediate(node.gameObject);
            }
        }

    }
}
