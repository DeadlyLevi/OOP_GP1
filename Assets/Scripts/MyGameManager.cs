using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    static MyGameManager _instance;
    public static MyGameManager Instance { get { return _instance; } }


    public float defaultTimeScale;
    public GameObject charRef;

    void Awake()
    {
        defaultTimeScale = Time.timeScale;
        if(_instance == null)
        {
            _instance = this;
        }
    }
}
