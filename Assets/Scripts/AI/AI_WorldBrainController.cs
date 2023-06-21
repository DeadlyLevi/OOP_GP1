using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_WorldBrainController : MonoBehaviour
{
    static AI_WorldBrainController _instance;
    public static AI_WorldBrainController Instance { get { return _instance; } }

    public List<KeyValuePair<AI_Brain, int>> ExecutionList;

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
        ExecutionList = new List<KeyValuePair<AI_Brain, int>>();
    }

    void Update()
    {
        foreach(KeyValuePair<AI_Brain,int> pair in ExecutionList)
        {
            Debug.Log(pair.Value);
        }
    }

    public void AddAction(AI_Brain Brain, int Id)
    {
        ExecutionList.Add(new KeyValuePair<AI_Brain, int > (Brain, Id));
    }
}
