using UnityEngine;

namespace Westhouse.GPG221.AI.Navigation
{
    [RequireComponent(typeof(NavNode))]
    [ExecuteInEditMode]
    public class NavNodeDeleteCatcher : MonoBehaviour
    {
        void OnDestroy()
        {
            NavNode node = GetComponent<NavNode>();
            NavUtil.PluckNode(node);
            NavUtil.activeSolver?.DeRegisterNavNodes(new NavNode[] {node});
        }
    }
}