using UnityEngine;
using UnityEngine.Events;

namespace GPG221.AI
{

    public class Strategy : MonoBehaviour
    {
        

        [System.Serializable]
        public class State
        {
            public WeightedBehavior[] actionBehaviours;

            public virtual void OnStateEnter(){}

            public virtual void OnStateExit(){}

            public virtual void OnStateContinue(){}
        }
    }

}