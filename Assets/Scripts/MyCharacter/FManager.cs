using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FManager : MonoBehaviour
{
    private static FManager _instance;
    public static FManager Instance { get => _instance; private set => _instance = value; }

    public List<MyFeature> allMyF;
    public List<MyFeature> selMyF;
    //public MyFeature[] selectedFeatures = new MyFeature[5];
    private int currentFeatureIndex;

    [SerializeField] private GameObject characterRef;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        allMyF = new List<MyFeature>();
    }

    public void Start()
    {
        characterRef = FindObjectOfType<MyCharacterMovement>().gameObject;
    }

    public void AddFeature(GeneralFSO feature)
    {
        string Fname = feature.featureName;
        Component componentRef = characterRef.GetComponent(Fname);

        if (componentRef == null)
        {
            MyFeature script = characterRef.gameObject.AddComponent(Type.GetType(Fname)) as MyFeature;
            script.enabled = feature.startEnabled;
            script.featureSO = feature;

            allMyF.Add(script);
            int id = allMyF.Count - 1;
            FCanvasManager.Instance.AddFeatureInGrid(id, allMyF[id].featureSO.featureIcon);
            // set id/index allMyFeature.Count-1;
            if(allMyF.Count == 0)
            {
                currentFeatureIndex = 0;
            }
        }
        else if (componentRef.gameObject.activeSelf == false)
        {
            characterRef.GetComponent(Type.GetType(Fname)).gameObject.SetActive(true);
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
        if(currentFeatureIndex + x > allMyF.Count - 1)
        {
            currentFeatureIndex = 0;
        }
        else if(currentFeatureIndex + x < 0)
        {
            currentFeatureIndex = allMyF.Count - 1;
        }
        else
        {
            currentFeatureIndex += x;
        }
    }

    void UpdateFeatures()
    {
        for(int i = 0; i <= selMyF.Count - 1; i++)
        {
            if(i == currentFeatureIndex)
            {
                selMyF[i].enabled = true;
            }
            else
            {
                selMyF[i].enabled = false;
            }
        }
    }

    public string GetSelectedFeatureName()
    {
        if(selMyF.Count > 0)
        {
            return selMyF[currentFeatureIndex].featureSO.featureName;
        }
        return null;
    }
}

