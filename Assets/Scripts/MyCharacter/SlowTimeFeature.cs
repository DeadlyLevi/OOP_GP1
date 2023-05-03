using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTimeFeature : MyFeature
{
    public bool isSlow;
    public float step = 0.02f;
    public float currentTime;
    void Start()
    {

    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        if (isSlow == true)
        {
            Slow();
        }

        SlowingTime();
        Debug.Log(Time.timeScale);
    }

    void SlowingTime()
    {
        if (isSlow && Input.GetKeyDown(KeyCode.L))
        {
            Time.timeScale = 1;
            isSlow = false;
            currentTime = 0;
        }
        else if (!isSlow && Input.GetKeyDown(KeyCode.L))
        {
            isSlow = true;
        }
    }
    void Slow()
    {
        if (currentTime < 1)
        {
            currentTime += step;
        }
        Time.timeScale = Mathf.Lerp(1f, 0.1f, currentTime);
    }
}
