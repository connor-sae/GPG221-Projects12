using System.Collections.Generic;
using UnityEngine;

public class NavNode : MonoBehaviour
{
    public bool obstructed { get; private set; }

    public float radius;
    public LayerMask obstruction;

    public float h_cost; // hueristic
    public float g_cost; // travel cost

    public List<NavNode> linkedNodes = new();


    public void UpdateBounds()
    {
        obstructed = Physics.OverlapSphere(transform.position, radius, obstruction).Length > 0;
    }

    public void ConnectToNode(NavNode otherNode)
    {
        if (linkedNodes.Contains(otherNode)) return; // do not add already linked nodes

        if(!otherNode.obstructed) // do not add obstructed nodes
            linkedNodes.Add(otherNode);
    }
}

