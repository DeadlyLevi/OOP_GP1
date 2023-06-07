using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClimbingFSO", menuName = "CustomScriptableObject/GeneralFSO/ClimbingFSO")]
public class ClimbingFSO : GeneralFSO
{
    [Header("Specific Info")]
    public float climbSpeed;

}
