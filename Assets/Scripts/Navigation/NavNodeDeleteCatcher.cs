using UnityEngine;

namespace GPG221.AI
{
    [RequireComponent(typeof(NavNode))]
    [ExecuteInEditMode]
    public class NavNodeDeleteCatcher : MonoBehaviour
    {
        void OnDestroy()
        {
            NavNode node = GetComponent<NavNode>();
            foreach(NavNode linked in node.linkedNodes)
            {
                linked.linkedNodes.Remove(node);
            }
            NavUtil.activeSolver.DeRegisterNavNodes(new NavNode[] {node});
        }
    }
}