using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class NavGrid : MonoBehaviour
{
    private new BoxCollider collider;

    [SerializeField] private Vector2Int gridResolution = new Vector2Int(10, 10);
    [SerializeField] private float NodeRadiusFactor = 1;
    [SerializeField] private bool allowDiagonals = true;
    [SerializeField] private LayerMask obstructionMask;

    private NavNode[,] nodes;

    public void Generate()
    {
        
        nodes = new NavNode[gridResolution.x, gridResolution.y];

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
                NavNode node = new GameObject($"Nav Node ({x}, {y})", typeof(NavNode)).GetComponent<NavNode>();
                node.transform.parent = transform;

                //set position
                float xPos = bounds.min.x + (0.5f + x) * dX;
                float zPos = bounds.min.z + (0.5f + y) * dZ;
                node.transform.position = new Vector3(xPos, transform.position.y, zPos);


                //test if obstructed
                node.radius = NodeRadiusFactor * (dX + dZ) * 0.25f;
                node.obstruction = obstructionMask;
                node.UpdateBounds();

                //connect to EXISTING previous nodes
                if(x > 0)
                {
                    Navigation.StitchNodes(node, nodes[x-1, y]);

                    if(y > 0)
                    {
                        Navigation.StitchNodes(node, nodes[x, y-1]);
                    }

                    if (allowDiagonals)
                    {
                        if (y + 1 < gridResolution.y)
                            Navigation.StitchNodes(node, nodes[x - 1, y + 1]);

                        if (y  > 0)
                            Navigation.StitchNodes(node, nodes[x - 1, y - 1]);
                    }
                }
                else
                if(y  > 0)
                {
                    Navigation.StitchNodes(node, nodes[x, y-1]);
                }

                nodes[x, y] = node;
            }
        }


    }

    public void Clear()
    {

        foreach (NavNode node in GetComponentsInChildren<NavNode>())
        {
            DestroyImmediate(node.gameObject);
            nodes = new NavNode[0,0];
        }
    }
}
