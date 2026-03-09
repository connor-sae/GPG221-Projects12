using UnityEngine;
using UnityEngine.Events;

namespace GPG221.AI
{

    public class Strategy : MonoBehaviour
    {
        

        [System.Serializable]
        public class Action
        {
            public WeightedBehavior[] actionBehaviours;
            public UnityAction onActionStart;
        }
    }

}