using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GliderFSO", menuName = "CustomScriptableObject/GeneralFSO/GliderFSO")]
public class GliderFSO : GeneralFSO
{
    [Header("Specific Info")]
    public float gliderSpeed = 10;
    public float rotationRate = 10;
    public float fallingSpeed = 10;
}
