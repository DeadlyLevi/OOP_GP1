using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Brain : MonoBehaviour
{
    List<int> actions;
    int id;
    void Start()
    {
        id = MyGameManager.Instance.AssignID();

        //AI_WorldBrainController.Instance.AddAction(this, id);
    }

    void Update()
    {
        Debug.Log(AI_Action.Instance.CallFunction(this));
    }
}
