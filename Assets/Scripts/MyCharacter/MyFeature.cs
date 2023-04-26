using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFeature : MonoBehaviour
{
    [Header("Settings")]
    public MyFeature feature;
    public bool disableGameObjectWhenPicked;
    public bool setFeatureActiveWhenPicked;

    private void Start()
    {
        FeatureManager fm;
        TryGetComponent<FeatureManager>(out fm);
        
        if(fm)
        {
            fm.featureList.Add(this);
        }
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //Attach Component of type feature
        if (other.CompareTag("Player"))
        {
            Component componentRef = other.GetComponent(feature.GetType());

            if (componentRef == null)
            {
                other.gameObject.AddComponent(feature.GetType());
            }
            else if (componentRef.gameObject.activeSelf == false)
            {
                other.GetComponent(feature.GetType()).gameObject.SetActive(true);
            }

            if (disableGameObjectWhenPicked)
                gameObject.SetActive(false);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {

    }
}
