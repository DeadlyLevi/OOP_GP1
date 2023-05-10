using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureManager : MonoBehaviour
{
    private static FeatureManager instance;
    public static FeatureManager Instance { get => instance; private set => instance = value; }

    public List<MyFeature> allMyFeatures;
    private int currentFeatureIndex;

    [SerializeField] private GameObject characterRef;

    private void Awake()
    {
        instance = this;
        allMyFeatures = new List<MyFeature>();
    }

    public void Start()
    {
        characterRef = FindObjectOfType<MyCharacterMovement>().gameObject;
    }

    public void AddFeature(MyFeature feature)
    {
        Component componentRef = characterRef.GetComponent(feature.GetType());

        if (componentRef == null)
        {
            MyFeature script = characterRef.gameObject.AddComponent(feature.GetType()) as MyFeature;
            script.enabled = false;
            allMyFeatures.Add(script);
            if(allMyFeatures.Count == 0)
            {
                currentFeatureIndex = 0;
            }
        }
        else if (componentRef.gameObject.activeSelf == false)
        {
            characterRef.GetComponent(feature.GetType()).gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        InputFeatureChange();
    }

    bool hasInput = false;
    void InputFeatureChange()
    {
        float v = Input.GetAxis(GameConstants.k_MouseScrollWheel);
        if(v > 0)
        {
            SelectFeature(1);
            hasInput = true;
        }
        else if(v < 0)
        {
            SelectFeature(-1);
            hasInput = true;
        }

        if(v != 0 && hasInput)
        {
            UpdateFeatures();
            hasInput = false;
        }
    }

    void SelectFeature(int x)
    {
        if(currentFeatureIndex + x > allMyFeatures.Count - 1)
        {
            currentFeatureIndex = 0;
        }
        else if(currentFeatureIndex + x < 0)
        {
            currentFeatureIndex = allMyFeatures.Count - 1;
        }
        else
        {
            currentFeatureIndex += x;
        }
    }

    void UpdateFeatures()
    {
        for(int i = 0; i <= allMyFeatures.Count - 1; i++)
        {
            if(i == currentFeatureIndex)
            {
                allMyFeatures[i].enabled = true;
            }
            else
            {
                allMyFeatures[i].enabled = false;
            }
        }
    }

    public string GetSelectedFeatureName()
    {
        if(allMyFeatures.Count > 0)
        {
            return allMyFeatures[currentFeatureIndex].name;
        }
        return null;
    }
}

