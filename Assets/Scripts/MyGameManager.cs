using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    static MyGameManager _instance;
    public static MyGameManager Instance { get { return _instance; } }

    [HideInInspector]
    public float defaultTimeScale;
    [HideInInspector]
    public GameObject charRef;

    public int enemiesInScene = 0;

    void Awake()
    {
        defaultTimeScale = Time.timeScale;
        if(_instance == null)
        {
            _instance = this;
        }
    }

    public int AssignID()
    {
        return enemiesInScene++;
    }
}
