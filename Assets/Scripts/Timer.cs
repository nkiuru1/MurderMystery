using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Timer : MonoBehaviour
{
    private float TargetTime;
    private bool TimeOver = false;
    private bool active = false;

    public void SetTime(float time)
    {
        this.TargetTime = time;
    }
    void Update()
    {
        if (active)
        {
            TargetTime -= Time.deltaTime;
            Debug.Log(TargetTime);
            if (TargetTime <= 0.0f)
            {
                TimeOver = true;
            }
        }

    }
    public bool IsTimeOver()
    {
        return TimeOver;
    }

}

