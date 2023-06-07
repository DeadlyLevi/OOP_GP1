using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GeneralFSO : ScriptableObject
{
    [SerializeField] string id;

    public string ID
    {
        get { return id; }
    }

    [Header("General Info")]
    public string featureName;
    public bool startEnabled;
    public Sprite featureIcon;


    void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }

}
