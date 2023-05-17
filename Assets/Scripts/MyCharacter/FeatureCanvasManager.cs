using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FeatureCanvasManager : MonoBehaviour
{
    private static FeatureCanvasManager _instance;
    public static FeatureCanvasManager Instance { get => _instance; private set => _instance = value; }

    public Text selectedFeature;
    public GameObject spriteFeature;
    public GameObject grid;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Update()
    {
        selectedFeature.text = FeatureManager.Instance.GetSelectedFeatureName();
    }

    public void AddFeatureInGrid(int Id)
    {
        GameObject newSprite = Instantiate(spriteFeature, grid.transform);
        newSprite.GetComponent<spriteFeatureInfo>().id = Id;
    }
}
