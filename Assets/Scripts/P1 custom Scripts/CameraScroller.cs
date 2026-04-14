using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace Westhouse.GPG221
{

    public class CameraScroller : MonoBehaviour
    {
        [SerializeField] private List<CinemachineCamera> VCams;
        public int activeCamIndex{get; private set;}
        private CinemachineCamera activeCamera
        {
            get
            {
                return VCams[activeCamIndex];
            }
        }

        private void Awake() {
            activeCamera.Priority = 1;
        }

        public void Scroll(int direction)
        {
            if(VCams.Count <= 0)
                return;

            activeCamera.Priority = 0;

            activeCamIndex += direction;
            if(activeCamIndex < 0) activeCamIndex = VCams.Count - 1;
            if(activeCamIndex >= VCams.Count) activeCamIndex = 0;

            activeCamera.Priority = 1;
        }

        public void AddCam(CinemachineCamera VCam)
        {
            VCams.Add(VCam);
        }

        public void RemoveCam(CinemachineCamera VCam)
        {
            Debug.LogWarning("Not implemented");
        }
    }
}
