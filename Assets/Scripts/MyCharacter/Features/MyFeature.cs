using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyFeature : MonoBehaviour
{

    [Header("Settings")]
    public GeneralFSO featureSO;
    public bool SetFalseWhenPicked;


    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {

    }

    protected virtual void Update()
    {
        
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //Attach Component of type feature
        if (other.CompareTag("Player"))
        {
            FManager.Instance.AddFeature(featureSO);

            if (SetFalseWhenPicked)
                gameObject.SetActive(false);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {

    }
}
