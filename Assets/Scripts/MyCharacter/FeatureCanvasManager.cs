using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FeatureCanvasManager : MonoBehaviour
{
    public Text selectedFeature;

    private void Update()
    {
        selectedFeature.text = FeatureManager.Instance.GetSelectedFeatureName();
    }
}
