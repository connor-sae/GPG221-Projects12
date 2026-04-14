using UnityEngine;

namespace Westhouse
{
    public class KillBox : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
        }
    }
}
