using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

namespace GPG221.AI.Solvers
{

    public class AStarSolver : NavSolver
    {

        [Header("A*")]
        public HueristicType hueristicSolver;
        private NavNode targetNode;

        protected override void Awake()
        {
            base.Awake();
            targetNode = new GameObject("Target Nav node", typeof(NavNode)).GetComponent<NavNode>();
        }

        protected List<NavNode> openNodes;
        protected List<NavNode> closedNodes;
        int solverCallID = 0;

        public override void GeneratePath(Vector3 origin, Vector3 target, out NavPath navPath)
        {
            solverCallID++;
            if(solverCallID >= int.MaxValue)
                solverCallID = 0;
            navPath = new();

            targetNode.transform.position = target;

            //// Stitch Target node to nearby nodes
            
            if(!NavUtil.StitchByDistance(targetNode, targetGridStitchThreshold))
            {
                // no nodes found / stitched within distance
                navPath.Fail("No Nodes found near target in threshold");
                return;
            } 

            //// Open the First Nodes Near the origin (previousnode value will be null to show chain end)
            
            openNodes = new();
            closedNodes = new();
            NavNode[] startNodes = NavUtil.GetNodesInDistance(origin, originGridStitchThreshold);

            if(startNodes.Length <= 0)
            {
                navPath.Fail("No Nodes found near origin in threshold");
                return;
            }
            
            foreach(NavNode startNode in startNodes)
            {
                startNode.GenerateCost(origin, 0, target, solverCallID);
                AddInAscOrder(startNode, openNodes);
            }

            NavNode currentNode = openNodes[0];
            closedNodes.Add(currentNode);
            openNodes.RemoveAt(0);

            while(currentNode != targetNode)
            {
                
                foreach(NavNode linkedNode in currentNode.linkedNodes)
                {
                    if(closedNodes.Contains(linkedNode))
                        continue;

                    if(linkedNode.GenerateCost(currentNode, target, solverCallID)) // if shorter and regenreated, will always run for an unopened node
                    {
                        if(openNodes.Contains(linkedNode)) // if already generated
                            openNodes.Remove(linkedNode); // reorder the regenerated node
                        
                        AddInAscOrder(linkedNode, openNodes); // add to open
                    }

                }

                if(openNodes.Count <= 0)
                {
                    navPath.Fail("Path could not be resolved: Not Connetcted");
                    return;
                }

                currentNode = openNodes[0]; // get shortest costing node
                closedNodes.Add(currentNode);
                openNodes.RemoveAt(0);

            }

            // current node is target node!!
            // collect path by moving backwards
            List<NavNode> nodePath = new();
            List<Vector3> pointPath = new();
            while(currentNode.previousNode != null)
            {
                nodePath.Insert(0, currentNode);
                pointPath.Insert(0, currentNode.transform.position);
                currentNode = currentNode.previousNode;
            }

            if(!excludeFirstNode)
            {
                nodePath.Insert(0, currentNode);
                pointPath.Insert(0, currentNode.transform.position);
            }

            navPath.SetPathPoints(nodePath.ToArray(), pointPath.ToArray()); // finished!!
            NavUtil.PluckNode(targetNode);
            return;
        }

        private void AddInAscOrder(NavNode node, List<NavNode> list)
        {
            if(list.Count <= 0)
            {
                list.Add(node);
                return;
            }

            for(int i = 0; i < list.Count; i++)
            {
                if(node.f_cost < list[i].f_cost) // correct position
                {
                    list.Insert(i, node);
                    return;
                }
            } // longest value (insert at end)

            list.Add(node);
        }

        public float GenerateHueristic(Vector3 a, Vector3 b)
        {
            switch (hueristicSolver)
            {
                case HueristicType.EUCLIDEAN:
                    return Vector3.Distance(a, b);

                case HueristicType.MANHATTAN:
                    return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z);

                default: return Mathf.Infinity;
            }
        }

    }

    public enum HueristicType
    {
        /// <summary>
        /// direct 3D distance between the points (expensive)
        /// </summary>
        EUCLIDEAN,
        /// <summary>
        /// Un-Normalised distance between points (cheap)
        /// </summary>
        MANHATTAN
    }

    
}
