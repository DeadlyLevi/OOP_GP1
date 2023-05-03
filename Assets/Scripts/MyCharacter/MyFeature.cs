using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyFeature : MonoBehaviour
{
    [Header("Settings")]
    public MyFeature feature;
    public bool SetFalseWhenPicked;
    public bool isUsed;


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
            FeatureManager.Instance.AddFeature(feature);

            if (SetFalseWhenPicked)
                gameObject.SetActive(false);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {

    }
}
