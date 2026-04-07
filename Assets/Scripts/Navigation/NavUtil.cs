using UnityEngine;
using GPG221.AI.Solvers;
using System.Collections.Generic;

namespace GPG221.AI
{

    public static class NavUtil
    {
        public static bool diag;
        public static NavSolver activeSolver
        {
            get
            {
                if(NavSolver.instance == null)
                {
                    NavSolver.instance = GameObject.FindAnyObjectByType<NavSolver>();
                }
                    
                return NavSolver.instance;
            }
        }

        /// <summary>
        /// Stitches 2 nodes together
        /// </summary>
        /// <param name="navNode1">first node</param>
        /// <param name="navNode2">second node</param>
        /// <param name="ignoreObstructed">do not stich nodes witch are obstructed</param>
        public static void StitchNodes(NavNode navNode1, NavNode navNode2, bool ignoreObstructed = true)
        {
            if (ignoreObstructed && (navNode1.obstructed || navNode2.obstructed))
                return;

            navNode1.ConnectToNode(navNode2);
            navNode2.ConnectToNode(navNode1);
        }

        /// <summary>
        /// Stitches all nav nodes specified to Eachother
        /// </summary>
        /// <param name="ignoreObstructed">do not stich nodes witch are obstructed</param>
        /// <param name="nodes"></param>
        public static void StitchNodes(bool ignoreObstructed = true, params NavNode[] nodes)
        {
            if (nodes.Length <= 1)
                return;

            for (int i = 0; i < nodes.Length; i++) 
            {
                for (int j = i+1; j < nodes.Length; j++)
                {
                    if (ignoreObstructed && (nodes[i].obstructed || nodes[j].obstructed))
                        continue;

                    nodes[i].ConnectToNode(nodes[j]);
                    nodes[j].ConnectToNode(nodes[i]);
                }
            }
        }

        /// <summary>
        /// Stitches one node to all others specified
        /// </summary>
        /// <param name="fromNode"></param>
        /// <param name="toNodes"></param>
        /// <param name="ignoreObstructed"></param>
        public static void StitchNodes(NavNode fromNode, NavNode[] toNodes, bool ignoreObstructed = true)
        {
            foreach(NavNode toNode in toNodes)
            {
                StitchNodes(fromNode, toNode, ignoreObstructed);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originNode"></param>
        /// <param name="distance"></param>
        /// <param name="ignoreObstructed"></param>
        /// <returns>if any nodes were found within the distance</returns>
        public static bool StitchByDistance(NavNode originNode, float distance, bool ignoreObstructed = true)
        {
            //List<NavNode> nearNodes = new();
            bool foundNode = false;
            foreach(NavNode node in activeSolver.navNodes)
            {
                if(node == originNode) continue;

                if(Vector3.Distance(originNode.transform.position, node.transform.position) <= distance)
                {
                    foundNode = true;
                    StitchNodes(originNode, node, ignoreObstructed);
                }
            }

            return foundNode;
        }

        public static NavNode[] GetNodesInDistance(Vector3 origin, float distance, bool ignoreObstructed = true)
        {
            List<NavNode> nodes = new();
            foreach(NavNode node in activeSolver.navNodes)
            {

                if(Vector3.Distance(origin, node.transform.position) <= distance)
                {
                    nodes.Add(node);
                }
            }
        
            return nodes.ToArray();
        }

        public static NavNode GetNearestNode(Vector3 from, NavNode[] ignoreNodes, bool ignoreObstructed = true)
        {
            NavNode[] nodes = activeSolver.navNodes;

            if(nodes == null)
            {
                Debug.LogError("Active navsolver does not contain any nodes! make sure they are assigned");
                return null;
            }

            NavNode nearNode = nodes[0];
            float nearestDistance = Mathf.Infinity;
            
            for(int i = 1; i < nodes.Length; i++)
            {
                if(nodes[i].obstructed && ignoreObstructed) // ignore obstructed nodes
                    continue;
                
                
                
                float nodeDist = Vector3.Distance(from, nodes[i].transform.position);
                if(nodeDist < nearestDistance) // is nearer than previous nearest distance
                {
                    bool ignore = false;
                    foreach(NavNode ignoreNode in ignoreNodes)
                    {
                        if(nodes[i] == ignoreNode)
                        {
                            ignore = true;
                            break;
                        }
                    }

                    if(ignore) continue; // if in our ignore list skip

                    nearestDistance = nodeDist;
                    nearNode = nodes[i];
                }
            }

            if(nearNode.obstructed && ignoreObstructed)
            {
                Debug.LogError("No unobstructed nodes found in active solver!");
                return null;
            }

            return nearNode;
        }

        public static NavNode GetRandomNode()
        {
            return activeSolver.navNodes[Random.Range(0, activeSolver.navNodes.Length)];
        }

        public static void SetActiveSolver(NavSolver solver)
        {
            NavSolver.instance = solver;
        }

        public static void SeverNodes(NavNode[] nodes)
        {
            if (nodes.Length <= 1)
                return;

            for (int i = 0; i < nodes.Length; i++) 
            {
                for (int j = i+1; j < nodes.Length; j++)
                {
                    nodes[i].linkedNodes.Remove(nodes[j]);
                    nodes[j].linkedNodes.Remove(nodes[i]);
                }
            }
        }

        public static void PluckNode(NavNode node)
        {
            foreach(NavNode linkedNode in node.linkedNodes)
            {
                linkedNode.linkedNodes.Remove(node);
            }
            node.linkedNodes = new();
        }
}
}
