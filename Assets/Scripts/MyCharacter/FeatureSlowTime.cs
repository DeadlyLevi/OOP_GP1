using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureSlowTime : MyFeature
{
    public float slowedTime = 0.5f;
    float defaultTime = 1f;

    public float targetTimeScale;

    public float step = 0.02f;

    bool bIsEnabled = false;
    bool isLerping = false;

    private void Awake()
    {
        defaultTime = Time.timeScale;
    }

    protected override void Update()
    {

        Debug.Log(Time.timeScale);
        if (Input.GetButtonDown(GameConstants.k_ButtonNameActivate))
        {
            bIsEnabled = !bIsEnabled;
            FlipFlopTargetTimeSpeed();
        }

        if(isLerping)
        {
            ChangeTimeScale();
        }
    }

    void FlipFlopTargetTimeSpeed()
    {
        if(bIsEnabled)
        {
            targetTimeScale = slowedTime;
            step = -Mathf.Abs(step);
        }
        else
        {
            targetTimeScale = defaultTime;
            step = Mathf.Abs(step);
        }
        isLerping = true;
    }

    void ChangeTimeScale()
    {
        if (Time.timeScale != targetTimeScale && bIsEnabled && Time.timeScale > targetTimeScale) //1f -> 0.4f
        {
            Time.timeScale += step;
        }
        else if (Time.timeScale != targetTimeScale && !bIsEnabled && Time.timeScale < targetTimeScale)
        {
            Time.timeScale += step;
        }
        else
        {
            isLerping = false;
        }
    }
}
