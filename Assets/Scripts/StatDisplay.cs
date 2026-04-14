using Anthill.AI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Westhouse.GPG221.AI.Agent;

namespace Westhouse.GPG221.AI.Strategy
{
    public class StatDisplay : MonoBehaviour
    {
        [SerializeField] private Slider hungerSlider;
        [SerializeField] private Slider maturitySlider;
        [SerializeField] private TMP_Text stateText;

        private SurvivalAgent sAgent;
        private AntAIAgent aAgent;

        void Start()
        {
            sAgent = GetComponent<SurvivalAgent>();
            aAgent = GetComponent<AntAIAgent>();
        }

        void Update()
        {
            hungerSlider.value = sAgent.hunger;
            maturitySlider.value = sAgent.maturity;
            stateText.text = aAgent.currentPlan[0];
        }
    }
}