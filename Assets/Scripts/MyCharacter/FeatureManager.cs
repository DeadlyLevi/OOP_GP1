using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureManager : MonoBehaviour
{
    private static FeatureManager instance;
    public static FeatureManager Instance { get => instance; private set => instance = value; }

    private List<MyFeature> features;
    private MyFeature currentFeature;

    [SerializeField] private GameObject player;

    private void Awake()
    {
        instance = this;
        features = new List<MyFeature>();
    }

    public void AddFeature(MyFeature feature)
    {
        Component componentRef = player.GetComponent(feature.GetType());

        if (componentRef == null)
        {
            MyFeature script = player.gameObject.AddComponent(feature.GetType()) as MyFeature;
            script.enabled = false;
            features.Add(script);
        }
        else if (componentRef.gameObject.activeSelf == false)
        {
            player.GetComponent(feature.GetType()).gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        int numberPressed = GetPressedNumber();

        if (numberPressed == -1) return;

        numberPressed--;

        if(numberPressed >= features.Count) return;

        if (currentFeature)
        {
            currentFeature.enabled = false;
        }

        MyFeature featureSelected = features[numberPressed];
        featureSelected.enabled = true;

        currentFeature = featureSelected;
    }

    private int GetPressedNumber()
    {
        for (int number = 1; number <= 3; number++)
        {
            if (Input.GetKeyDown(number.ToString()))
                return number;
        }

        return -1;
    }
}
