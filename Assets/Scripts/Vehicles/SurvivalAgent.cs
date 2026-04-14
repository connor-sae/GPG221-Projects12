using UnityEngine;
using Westhouse.GPG221.AI.Agent;

namespace Westhouse.GPG221.AI.Strategy
{
    [RequireComponent(typeof(ViewConeSense))]
    public class SurvivalAgent : Agent.Agent
    {
        public float hunger = 1;
        public float maturity = 0;

        [SerializeField] float hungerRuductionRate = 0.05f;
        [SerializeField] float maturationRate = 0.05f;

        public float hungryThreshold = 0.6f;
        public float starvingThreshold = 0.3f;
        [SerializeField] private bool dieFromStarvation = true;

        [HideInInspector] public SurvivalAgent targetMate;
        public float matingDistance = 1f;
        [SerializeField] private string mateTag;

        private ViewConeSense viewCone;

        protected override void Awake()
        {
            base.Awake();
            viewCone = GetComponent<ViewConeSense>();
        }


        protected override void Update()
        {
            base.Update();

            hunger -= hungerRuductionRate * Time.deltaTime;
            if(hunger < 0) 
                if(dieFromStarvation)
                    Destroy(gameObject);
                else
                    hunger = 0;

            maturity += maturationRate * Time.deltaTime;
            if(maturity > 1) maturity = 1;

            visual.transform.localScale = Vector3.one * (maturity * 0.5f + 0.5f); // display maturity as scale

            if(!IsHungry())
            {
                if(targetMate != null)
                    if(targetMate.IsHungry())
                        targetMate = null; // if the other mate becomes hungry, abandon

            }else // if hungry abandon
                targetMate = null;
        }

        public bool IsHungry()
        {
            return hunger < hungryThreshold;
        }

        public bool IsMature()
        {
            return maturity >= 1;
        }

        public bool IsStarving()
        {
            return hunger < starvingThreshold;
        }
    }
}
