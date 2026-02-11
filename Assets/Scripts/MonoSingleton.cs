using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T I;
    public static T instance
    {
        get
        {
            //create a new instance if it does not exist in the scene
            if(I == null)
            {
                I = new GameObject("New " + typeof(T), typeof(T)).GetComponent<T>();
            }

            return I;
        }
    }

    void Awake()
    {
        if(I == null)
        {
            I = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

