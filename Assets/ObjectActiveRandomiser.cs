using UnityEngine;

public class ObjectActiveRandomiser : MonoBehaviour
{
    [SerializeField] private float activeRate = 0.3f;

    [SerializeField] private GameObject[] objects;

    public void Randomise()
    {
        foreach(GameObject obj in objects)
        {
            if(Random.Range(0, 1f) < activeRate)
            {
                obj.SetActive(true);
            }else
                obj.SetActive(false);
        }
    }

    public void Clear()
    {
        foreach(GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }
}
