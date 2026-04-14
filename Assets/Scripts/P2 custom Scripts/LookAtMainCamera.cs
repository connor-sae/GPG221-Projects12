using UnityEngine;

namespace Westhouse
{
    public class LookAtMainCamera : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}