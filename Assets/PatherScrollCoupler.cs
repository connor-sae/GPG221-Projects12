using System.IO;
using UnityEngine;

[RequireComponent(typeof(CameraScroller))]
public class PatherScrollCoupler : MonoBehaviour
{
    CameraScroller scroller;
    int oldIndex;
    [SerializeField] private GameObject[] pathers;
    void Start()
    {
        scroller = GetComponent<CameraScroller>();
        foreach(GameObject pather in pathers) pather.SetActive(false);
        pathers[scroller.activeCamIndex].SetActive(true);
    }
    void Update()
    {
        if(scroller.activeCamIndex != oldIndex)
        {
            
            pathers[oldIndex].SetActive(false);
            pathers[scroller.activeCamIndex].SetActive(true);

            oldIndex = scroller.activeCamIndex;
        }
    }
}
