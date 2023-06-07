using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowtimeF : MyFeature
{

    public float slowedTime;
    public float step = 0.02f;



    float targetTimeScale;
    float defaultTime;
    bool bIsEnabled = false;
    bool isLerping = false;

    SlowTimeFSO SO;
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void Awake()
    {
        defaultTime = Time.timeScale;
    }

    private void Start()
    {
        SO = featureSO as SlowTimeFSO;
        slowedTime = SO.slowedTime;
        step = SO.step;
        targetTimeScale = slowedTime;
    }

    protected override void Update()
    {
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
