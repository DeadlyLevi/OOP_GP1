using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlowtimeFSO", menuName = "CustomScriptableObject/GeneralFSO/SlowtimeFSO")]
public class SlowTimeFSO : GeneralFSO
{
    public float slowedTime = 0.5f;
    public float step = 0.02f;
}
