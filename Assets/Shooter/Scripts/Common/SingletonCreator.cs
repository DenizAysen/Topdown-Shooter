using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonCreator<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T instance;
    protected static bool isQuiting;
    public static T Instance
    {
        get 
        {
            if (instance == null && !isQuiting)
            {
                instance = FindObjectOfType(typeof(T)) as T;
                if(instance == null)
                {
                    GameObject singletonGMO = new GameObject(typeof(T).Name);
                    instance = singletonGMO.AddComponent<T>();
                }
            }
            return instance;
        }
    }
    public static bool HasInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;
            }
            return instance != null;
        }
    }
    //protected virtual void Awake()
    //{
    //    if (instance != null && instance != this) 
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }
    //    instance = GetComponent<T>();
    //    //DontDestroyOnLoad(instance);
    //}
    protected virtual void OnApplicationQuit()
    {
        isQuiting = true;
    }
}
