using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureManager : MonoBehaviour
{
    public List<MyFeature> featureList;

    int lastFeatureUsedIndex;
    MyCharacterMovement mcm;

    private void Start()
    {
        lastFeatureUsedIndex = 0;
        mcm = FindObjectOfType<MyCharacterMovement>();
    }

    private void Update()
    {
        Inputs();
    }

    void AddFeature(MyFeature feat)
    {
        mcm.gameObject.AddComponent(feat.GetType());
    }

    void Inputs()
    {
        int index;
        if (Input.GetButtonDown(GameConstants.k_ButtonA1))
        {
            index = 0;
            if(featureList[index] != null)
            {
                featureList[lastFeatureUsedIndex].enabled = false;
                featureList[index].enabled = true;
                lastFeatureUsedIndex = index;
            }
            
        }

        if (Input.GetButtonDown(GameConstants.k_ButtonA1))
        {
            index = 1;
            if (featureList[index] != null)
            {
                featureList[index].enabled = true;
                featureList[lastFeatureUsedIndex].enabled = false;
                lastFeatureUsedIndex = index;
            }
        }

        if (Input.GetButtonDown(GameConstants.k_ButtonA1))
        {
            index = 2;
            if (featureList[index] != null)
            {
                featureList[index].enabled = true;
                featureList[lastFeatureUsedIndex].enabled = false;
                lastFeatureUsedIndex = index;
            }
        }
    }

}
