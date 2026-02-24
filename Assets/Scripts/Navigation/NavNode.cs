using System.Collections.Generic;
using System.Linq.Expressions;
using GPG221.AI;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(NavNodeDeleteCatcher))]
public class NavNode : MonoBehaviour
{
    public bool obstructed { get; private set; }
    
    public float obstructionRadius;
    public float pathObstructionFactor = 0.8f;
    public LayerMask obstructionMask;
    [HideInInspector] public NavNode lastNode;
    private int solveID = -1;

    public float h_cost; // hueristic
    public float g_cost; // travel cost
    public float f_cost
    {
        get
        {
            if(h_cost + g_cost <= 0)
                Debug.LogWarning("f_cost of node is 0, node cast may not be generated");
            return h_cost + g_cost;
        }
    }
    public NavNode previousNode; // previous node in node chain

    public List<NavNode> linkedNodes = new();
    // public int linkedNodeCount
    // {
    //     get
    //     {
    //         return linkedNodes.Count;
    //     }
    // }

    // public void GetlinkedNode(int index)
    // {
    //     if(linkedNodes[index] == null)
    //         linkedNodes.RemoveAt(index);
        

    // }

    /// <summary>
    /// Regenerates the f_cost and g_cost of this node
    /// </summary>
    /// <returns>returns true if the newly generated value is shorter and therefor updated</returns>
    /// <param name="navTarget">The ID of the solve call used to determine whether values must be regenerated (cannot be negative)</param>
    public bool GenerateCost(Vector3 fromNodePos, float base_g_cost, Vector3 navTarget, int solveCallID, NavNode previousLink = null)
    {  
        float new_g_cost = Vector3.Distance(fromNodePos, transform.position) + base_g_cost;
        
        if(solveID != solveCallID) // new path solver being called!! must be regenreated
        {
            g_cost = new_g_cost;
            h_cost = Vector3.Distance(navTarget, transform.position);

            lastNode = previousLink;
            return true;
        }

        if(new_g_cost < g_cost)
        {
            g_cost = new_g_cost;

            lastNode = previousLink;
            return true;
        }
        return false;
    }


    /// <summary>
    /// Regenerates the f_cost and g_cost of this node Automatically links previous node!
    /// </summary>
    /// <returns>returns true if the newly generated value is shorter and therefor updated</returns>
    /// <param name="navTarget">The ID of the solve call used to determine whether values must be regenerated (cannot be negative)</param>
    public bool GenerateCost(NavNode fromNode, Vector3 navTarget, int solveCallID)
    {  
        return GenerateCost(fromNode.transform.position, fromNode.g_cost, navTarget, solveCallID, fromNode);
    }

    public void UpdateBounds()
    {
        obstructed = Physics.OverlapSphere(transform.position, obstructionRadius, obstructionMask).Length > 0;
    }
    public void DisconnectObstructedConnections(float pathThinning = 0.1f)
    {
        if(!obstructed)
            for(int i = linkedNodes.Count - 1; i >= 0; i--)
            {
                if(NodeConnectionObstructed(linkedNodes[i]))
                {
                    linkedNodes.Remove(linkedNodes[i]);
                }
            }
    }

    public bool NodeConnectionObstructed(NavNode otherNode)
    {
        Vector3 direction = otherNode.transform.position - transform.position;

        return Physics.SphereCast(transform.position, obstructionRadius*pathObstructionFactor, direction, out RaycastHit hit, direction.magnitude, obstructionMask);
    }

    public void ConnectToNode(NavNode otherNode)
    {
        if (linkedNodes.Contains(otherNode)) return; // do not add already linked nodes

        if(!otherNode.obstructed && !NodeConnectionObstructed(otherNode)) // do not add obstructed nodes
            linkedNodes.Add(otherNode);
    }

    
}

