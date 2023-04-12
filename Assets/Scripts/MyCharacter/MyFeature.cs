using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFeature : MonoBehaviour
{
    [Header("Settings")]
    public MyFeature feature;

    protected virtual void Update()
    {
        
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //Attach Component of type feature
        if(other.CompareTag("Player"))
            other.gameObject.AddComponent(feature.GetType());
    }
}
