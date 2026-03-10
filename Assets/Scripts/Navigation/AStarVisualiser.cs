using GPG221.AI.Solvers;
using UnityEngine;

namespace GPG221.AI
{

    public class AStarVisualiser : AStarSolver
    {
        public override void GeneratePath(Vector3 origin, Vector3 target, out NavPath navPath)
        {
            base.GeneratePath(origin, target, out navPath);
            foreach (NavNode node in navNodes)
            {
                if(node is VisualisedNavNode && !node.obstructed)
                    (node as VisualisedNavNode).ExcludeVisual();
            }
            foreach (NavNode node in openNodes)
            {
                if(node is VisualisedNavNode)
                    (node as VisualisedNavNode).UpdateCostVisuals(false);
            }
            foreach (NavNode node in closedNodes)
            {
                if(node is VisualisedNavNode)
                {
                    (node as VisualisedNavNode).UpdateCostVisuals(true);
                    if(Vector3.Distance(target, node.transform.position) < 0.3f)
                        (node as VisualisedNavNode).image.color = Color.blue;
                }
            }
            foreach(NavNode node in navPath.nodes)
            {
                if(node is VisualisedNavNode)
                    (node as VisualisedNavNode).image.color = Color.green;
            }

        }
    }
}
