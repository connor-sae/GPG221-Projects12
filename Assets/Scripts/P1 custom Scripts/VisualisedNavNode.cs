using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Westhouse.GPG221.AI.Navigation
{
    public class VisualisedNavNode : NavNode
    {
        [SerializeField] private TMP_Text h_CostTMP;
        [SerializeField] private TMP_Text g_CostTMP;
        [SerializeField] private TMP_Text f_CostTMP;
        [SerializeField] private TMP_Text closed_TMP;
        [SerializeField] public Image image;

        public void ExcludeVisual()
        {
            image.color = Color.white;
        }

        public void UpdateCostVisuals(bool closed)
        {
            h_CostTMP.text = "H: " + h_cost.ToString();
            g_CostTMP.text = "G: " + g_cost.ToString();
            f_CostTMP.text = "F: " + f_cost.ToString();
            closed_TMP.text = closed ? "closed" : "open";
            image.color = closed ? Color.black : Color.gray;
        }

        public override void UpdateBounds()
        {
            base.UpdateBounds();

            if(obstructed)
                image.color = Color.red;
        }
    }
}