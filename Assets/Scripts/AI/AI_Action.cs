using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Action : MonoBehaviour
{
    static AI_Action _instance;
    public static AI_Action Instance { get { return _instance; } }
    //MOVE

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public Vector3 CallFunction(AI_Brain brain)
    {
        return Move(brain);
    }
    public Vector3 Move(AI_Brain brain)
    {
        return brain.gameObject.transform.position + brain.gameObject.transform.forward * 5f;
    }
}
