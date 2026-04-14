using Westhouse.GPG221.AI.Navigation;
using UnityEngine;

namespace Westhouse.GPG221.AI
{
    public class FoodScatter : MonoBehaviour
    {
        void Start()
        {
            Scatter();
        }

        void Scatter()
        {
            //set position to a random node
            NavNode ranNade = NavUtil.GetRandomNode();
            transform.position = ranNade.transform.position; 
        }

        public void Consume()
        {
            Scatter();
        }
    }
}